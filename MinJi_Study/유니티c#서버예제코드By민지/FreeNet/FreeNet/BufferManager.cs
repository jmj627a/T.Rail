using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

//이 클래스는 각 소켓 I / O 작업마다 분할되어 사용 가능한 
//SocketAsyncEventArgs 객체에 할당 될 수있는 단일 '대형 버퍼'를 만듭니다.
//이렇게하면 버퍼를 쉽게 재사용하고 힙 메모리 조각화를 방지 할 수 있습니다.


/// <summary>
/// 여기에는 생성자, 버퍼 공간 할당하는 init, set, free 함수가 있다. (internal이기 때문에 외부에서는 못 씀)
/// </summary>
namespace FreeNet
{
    internal class BufferManager //--> internal은 protected같은것. 외부에서 접근할 수 없다
    {
        int m_numBytes;             //버퍼 풀에 의해 제어되는 총 바이트 수
        byte[] m_buffer;            //버퍼 매니저에 의해 보관 유지되는 부하의 바이트 배열
        Stack<int> m_freeIndexPool; //후입선출 방식의 index pool
        int m_currentIndex;         //현재 인덱스
        int m_bufferSize;           //

        //생성자. 생성할 땐 항상 전체 바이트 숫자와, 버퍼 사이즈를 알려주어야 한다.
        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;
            m_currentIndex = 0;
            m_bufferSize = bufferSize;
            m_freeIndexPool = new Stack<int>();
        }

        //버퍼 풀에의해 사용될 버퍼 공간을 할당한다.
        public void InitBuffer()
        {
            //하나의 큰 버퍼를 생성하고, 각 SocketAsyncEventArg 오브젝트만큼 나눈다.
            m_buffer = new byte[m_numBytes];
        }

        //버퍼 풀에서 지정된 SocketAsyncEventArgs 객체로 버퍼를 할당합니다.
        //버퍼가 정상적으로 설정된 경우는 true, 그렇지 않은 경우는 false
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if(m_freeIndexPool.Count > 0)
            {
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else
            {
                if( (m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    return false;
                }

                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }


        // SocketAsyncEventArg 개체에서 버퍼를 제거합니다. 그러면 버퍼가 버퍼 풀로 다시 해제됩니다.
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }
    }
}
