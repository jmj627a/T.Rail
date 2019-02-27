using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace FreeNet
{
    class CNetworkService
    {
        //연결된 숫자 카운트
        int connected_count;

        //클라이언트의 접속을 받아들이기 위한 객체
        CListener client_listener;

        //메세지 수신 시 필요한 객체
        SocketAsyncEventArgsPool receive_event_args_pool;
        //메세지 전송 시 필요한 객체
        SocketAsyncEventArgsPool send_event_args_pool;

        //메세지 수신, 전송 시 닷넷 비동기 소켓에서 사용할 버퍼를 관리하는 객체
        BufferManager buffer_manager;

        //클라이언트의 접속이 이루어졌을 때 호출되는 델리게이트
        public delegate void SessionHandler(CUserTocken token);
        public SessionHandler session_created_callback { get; set; }


        int max_connections;                    //최대 접속 수
        int buffer_size;                        //버퍼 사이즈
        readonly int pre_alloc_count = 2;       //read, write

        //class 초기화 
        public CNetworkService()
        {
            this.connected_count = 0;                   //연결된 클라이언트 숫자 0으로 초기화
            this.session_created_callback = null;       //
        }


        // 재사용 가능한 버퍼를 미리 할당하여 서버를 초기화합니다.
        // 컨텍스트 개체.이 객체는 미리 할당 할 필요가 없습니다.
        // 또는 재사용 할 수 있지만 API가 어떻게
        // 재사용 가능한 객체를 쉽게 생성하여 서버 성능을 향상시킬 수 있습니다.
        public void initialize()
        {
            this.max_connections = 10000;
            this.buffer_size = 1024;

            this.buffer_manager = new BufferManager(this.max_connections * this.buffer_size * this.pre_alloc_count);
            this.receive_event_args_pool = new SocketAsyncEventArgsPool(this.max_connections);
            this.send_event_args_pool = new SocketAsyncEventArgsPool(this.max_connections);

            // 하나의 큰 바이트 버퍼를 할당합니다.
            this.buffer_manager.InitBuffer();

            //개체의 풀을 미리 할당합니다
            SocketAsyncEventArgs arg;

            for(int i=0; i<this.max_connections; i++)
            {
                //동일한 소켓에 대고 send, receive를 하므로
                //user token은 세션별로 하나씩만 만들어 놓고
                //receive, send, EventArgs에서 동일한 token을 참조하도록 구성한다.
                //어쨌든 send receive할때 이 CUserToken을 참조한다는 것.
                CUserToken token = new CUserToken();

                //receive pool
                {
                    //재사용 가능한 SocketAsyncEventArgs를 미리 할당
                    arg = new SocketAsyncEventArgs();
                    arg.Completed += new EventHandler<SocketAsyncEventArgs>(receive_completed);
                    arg.UserToken = token;

                    //버퍼 풀에서 SocketAsyncEventArg 개체로 바이트 버퍼 할당
                    this.buffer_manager.SetBuffer(arg);

                    //SocketAsyncEventArg 풀에 추가
                    this.receive_event_args_pool.Push(arg);
                }

                //send pool
                {
                    //재사용 가능한 SocketAsyncEventArgs를 미리 할당
                    arg = new SocketAsyncEventArgs();
                    arg.Completed += new EventHandler<SocketAsyncEventArgs>(send_completed);
                    arg.UserToken = token;

                    //버퍼 풀에서 SocketAsyncEventArg 개체로 바이트 버퍼 할당
                    this.buffer_manager.SetBuffer(arg);

                    //SocketAsyncEventArg 풀에 추가
                    this.send_event_args_pool.Push(arg);
                }
            }

        }


        //이 함수에서 맨 위에 선언했던 client_listener라는 변수를 초기화하는데 왜 클래스 초기화때 안하고, initalize때 안하고 지금 하는건지 모르겠네???
        public void listen(string host, int port, int backlog)
        {
            this.client_listener = new CListener();
            this.client_listener.callback_on_newclient += on_new_client;
            this.client_listener.start(host, port, backlog);
        }

        //원격 서버에 접속 성공 했을 떄 호출됩니다
        public void on_connect_completed(Socket socket, CUserToken token)
        {
            //SovketAsyncEventArgsPool에서 빼오지 않고 그때그때 할당해서 사용한다
            //왜? 풀은 서버에서 클라이언트와의 통신용으로만 쓰려고 만든것이기 떄문이다.
            //클라이언트 입장에서 서버와 통신을 할 때는 접속한 서버당 두 개의 EventArgs만 있으면 되기 때문에 
            //그냥 new해서 쓴다.
            //서버간 연결에서도 마찬가지이다.
            //풀링처리를 하려면 c->s로 가는 별도의 풀을 만들어서 써야한다.
            SocketAsyncEventArgs receive_event_arg = new SocketAsyncEventArgs();
            receive_event_arg.Completed += new EventHandler<SocketAsyncEventArgs>(receive_completed);
            receive_event_arg.UserToken = token;
            receive_event_arg.SetBuffer(new byte[1024], 0, 1024);

            SocketAsyncEventArgs send_event_arg = new SocketAsyncEventArgs();
            send_event_arg.Completed += new EventHandler<SocketAsyncEventArgs>(send_completed);
            send_event_arg.UserToken = token;
            send_event_arg.SetBuffer(new byte[1024], 0, 1024);

            begin_receive(socket, receive_event_arg, send_event_arg);
        }


        //새로운 클라이언트가 접속 성공 했을 때 호출됩니다.
        //AcceptAsync의 콜백 매소드에서 호출되어 여러 스레드에서 동시에 호출될 수 있기 때문에
        //공유자원에 접근할 때는 주의해야 합니다.
        void on_new_client(Socket client_socket, object token)
        {
            Interlocked.Increment(ref this.connect_count);

            Console.WriteLine(string.Format("[{0}] A client connected. handle {1},  count {2}",
                Thread.CurrentThread.ManagedThreadId, client_socket.Handle,
                this.connected_count));

            //플에서 하나 꺼내와 사용한다.
            SocketAsyncEventArgs receive_args = this.receive_event_args_pool.Pop();
            SocketAsyncEventArgs send_args = this.send_event_args_pool.Pop();

            CUserToken user_token = null;
            if(this.session_created_callback != null)
            {
                user_token = receive_event_args_pool.UserToken as CUserToken;
                this.session_created_callback(user_token.token);
            }

            begin_receive(client_socket, receive_args, send_args);
        }

        void begin_receive(Socket socket, SocketAsyncEventArgs receive_args, SocketAsyncEventArgs send_args)
        {
            // receive_args, send_args 아무곳에서나 꺼내와도 된다. 둘다 동일한 CUserToken을 물고 있다.
            CUserToken token = receive_args.UserToken as CUserToken;
            token.set_event_args(receive_args, send_args);

            //생성된 클라이언트 소켓을 보관해 놓고 통신할 때 사용한다.
            token.socket = socket;

            bool pending = socket.ReceiveAsync(receive_args);
            if(!pending)
            {
                process_receive(receive_args);
            }
        }

        //이 메소드는 소켓에서 수신 또는 전송 작업이 완료 될 때마다 호출됩니다.
        //근데 기능이 뭔데????
        void receive_completed(object sender, SocketAsyncEventArgs e)
        {
            if(e.LastOperation == SocketAsyncOperation.Receive)
            {
                process_receive(e);
                return;
            }

            throw new ArgumentException("The last operation completed on the socket was not a receive.");
        }

        //이 메소드는 소켓에서 수신 또는 전송 작업이 완료 될 때마다 호출됩니다.
        void send_completed(object sender, SocketAsyncEventArgs e)
        {
            CUserToken token = e.UserToken as CUserToken;
            token.process_send(e);
        }

        //이 메서드는 비동기 수신 작업이 완료 될 때 호출됩니다.
        //원격 호스트가 연결을 닫으면 소켓이 닫힙니다.
        private void process_receive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            CUserToken token = e.UserToken as CUserToken;
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                token.on_receive(e.Buffer, e.Offset, e.BytesTransferred);

                bool pending = token.socket.ReceiveAsync(e);
                if (!pending)
                {
                    //스택 오버플로우인가?
                    process_receive(e);
                }
            }
            else
            {
                Console.WriteLine(string.Format("error {0},  transferred {1}", e.SocketError, e.BytesTransferred));
                close_clientsocket(token);
            }
        }


        public void close_clientsocket(CUserToken token)
        {
            token.on_removed();

            // Free the SocketAsyncEventArg so they can be reused by another client
            // 버퍼는 반환할 필요가 없다. SocketAsyncEventArg가 버퍼를 물고 있기 때문에
            // 이것을 재사용 할 때 물고 있는 버퍼를 그대로 사용하면 되기 때문이다.
            if (this.receive_event_args_pool != null)
            {
                this.receive_event_args_pool.Push(token.receive_event_args);
            }

            if (this.send_event_args_pool != null)
            {
                this.send_event_args_pool.Push(token.send_event_args);
            }
        }
    }
}
