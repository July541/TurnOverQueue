using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Turnover_Queue
{
    public class TurnOverQueue<T>
    {
        protected Queue<T> m_frontQueue;
        protected Queue<T> m_backQueue;
        private static object _lock = new object();
        public int Add(T t)
        {
             lock(_lock)
             {
                 m_frontQueue.Enqueue(t);
                 return m_frontQueue.Count();
             }
        }
        private void Swap()
        {
            lock(_lock)
            {
                Queue<T> temp = new Queue<T>();
                temp = m_backQueue;
                m_backQueue = m_frontQueue;
                m_frontQueue = temp;
            }
        }
        public bool Get(out T t)
        {
            if(m_backQueue.Count() == 0)
            {
                Swap();
            }
            if(m_backQueue.Count() == 0)
            {
                t = default(T);
                return false;
            }
            t = m_backQueue.Dequeue();
            return true;
        }
        public TurnOverQueue()
        {
            m_frontQueue = new Queue<T>();
            m_backQueue = new Queue<T>();
        }
    }
}
