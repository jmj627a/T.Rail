using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace FreeNet
{
    class CListener
    {
        //비동기 accept를 위한 EventArgs
        SocketAsyncEventArgs accept_args; //using System.Net.Sockets;

        Socket listen_socket;

        //Accept처리의 순서를 제어하기 위한 이벤트 변수
        AutoResetEvent flow_control_event;

        //새로운 클라이언트가 접속했을 떄 호출되는 콜백
        public delegate void NewclientHandler(Socket client_socket, object token);
        public NewclientHandler callback_on_newclient;

        public CListener()
        {
            this.callback_on_newclient = null;
        }

        public void start(string host, int port, int backlog)
        {
            this.listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address;

            if(host == "0.0.0.0")
            {
                address = IPAddress.Any;
            }
            else
            {
                address = IPAddress.Parse(host);
            }
            IPEndPoint endpoint = new IPEndPoint(address, port);


            try
            {
                listen_socket.Bind(endpoint);
                listen_socket.Listen(backlog);

                this.accept_args = new SocketAsyncEventArgs();
                this.accept_args.Completed += new EventHandler<SocketAsyncEventArgs>(on_accept_completed);

                Thread listen_thread = new Thread(do_listen);
                listen_thread.Start();
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
            }
        }



        //루프를 돌며 클라이언트를 받아들입니다.
        //하나의 접속 처리가 완려된 후 다음 accept를 수행하기 위해서 event객체를 통해 흐름을 제어하도록 구현되어있다.
        //위에서 new Thread()할때 부르는 함수
        void do_listen()
        {
            this.flow_control_event = new AutoResetEvent(false);

            while(true)
            {
                //SocketAsyncEventArgs를 재사용 하기 위해서 null로 만들어 준다.
                this.accept_args.AcceptSocket = null;

                bool pending = true;

                try
                {
                    //비동기 accpet를 호출하여 클라이언트의 접속을 받아들입니다.
                    //비동기 매소드지만, 동기적으로 수행이 완료될 경우도 있으니 
                    //리턴값을 확인하여 분기시켜야 합니다.
                    pending = listen_socket.AcceptAsync(this.accept_args);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    continue;
                }

                //즉시 완료되면 이벤트가 발생하지 않으므로 리턴값이 false일 경우 콜백 메소드를 직접 호출해 줍니다.
                //pending상태라면 비동기 요청이 들어간 상태이므로 콜백 매소드를 기다리면 됩니다.
                if(!pending)
                {
                    on_accept_completed(null, this.accept_args);
                }

                //클라이언트 접속 처리가 완료되면 이벤트 객체의 신호를 전달받아 다시 루프를 수행하도록 합니다.
                this.flow_control_event.WaitOne();

                //반드시 waitOne()-> Set순서로 호출되어야 하는 것은 아님.
                //accept작업이 굉장히 빨리 끝나서 set->WaitOne 순서로 호출된다고 하더라도
                //다음 accept호출까지 문제 없이 이루어짐.
                //WaitOne메소드가 호출될 때 이벤트 객체가 이미 signalled상대라면 스레드를 대기하지 않고 계속 
                //진행하기 때문이다.
            }
        }


        //AccpetAsync의 콜백 메소드
        void on_accept_completed(object sender, SocketAsyncEventArgs e)
        {
            if(e.SocketError == SocketError.Success)
            {
                //새로 생긴 소켓을 보관해 놓은 뒤
                Socket client_socket = e.AcceptSocket;

                //다음 연결을 받아들인다.
                this.flow_control_event.Set();

                //이 클래스에서는 accept까지의 역할만 수행하고 클라이언트의 접속 이후의 처리는
                //외부로 넘기기 위해 콜백 메소드를 호출해주도록 한다.
                //왜?? 소켓처리부와 컨텐츠 구현부를 분리하기 위함!!
                //컨텐츠 구현부분은 자주 바뀔 가능성이 있지만, 소켓 accpet부분은 상대적으로 변경이 적은 부분이기떄문에
                //양쪽을 분리시켜주는 것이 좋다.
                //또, 클래스 설계 방침에 따라 Listen에관련된 코드만 존재하도록 하기 위한 이유도 있다.
                if (this.callback_on_newclient != null)
                {
                    this.callback_on_newclient(client_socket, e.UserToken);
                }

                return;
            }
            else
            {
                //Console.WriteLine("Failed to accept client.");
            }

            // 다음 연결을 받아들인다.
            this.flow_control_event.Set();
        }

        //원본에 이 밑에 있는 코드들은 뭘까??? 








    }
}
