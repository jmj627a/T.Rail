using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

//여기엔 풀에대한 초기화, Push, Pop, Count 함수가 들어있다.

namespace FreeNet
{
    // 재사용 가능한 SocketAsyncEventArgs 개체의 컬렉션을 나타냅니다.
    class SocketAsyncEventArgsPool
    {
        Stack<SocketAsyncEventArgs> m_pool;

        //오브젝트 pool을 지정된 사이즈로 초기화
        //'capacity'매개 변수는, 최대 풀이 보유 할 수있는 SocketAsyncEventArgs 개체
        public SocketAsyncEventArgsPool(int capacity)
        {
            m_pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        //SocketAsyncEventArgs 인스턴스를 플에 추가
        //"item"매개 변수는 풀에 추가 할 SocketAsyncEventArgs 인스턴스.
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (m_pool)
            {
                m_pool.Push(item);
            }
        }

        //풀에서 SocketAsyncEventArgs 인스턴스를 제거합니다.
        //풀에서 제거 된 객체를 반환합니다.
        public SocketAsyncEventArgs Pop()
        {
            lock(m_pool)
            {
                return m_pool.Pop();
            }
        }

        //SocketAsyncEventArgs 풀에 들어있는 인스턴스의 숫자 리턴
        public int Count
        {
            get { return m_pool.Count;  }
        }
    }
}
