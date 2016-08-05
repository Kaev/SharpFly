using System;
using System.Collections.Generic;

namespace SharpFly_Login.Utility
{
    class Pool<T>
    {
        private Stack<T> m_Pool;

        public Pool(Int32 capacity)
        {
            this.m_Pool = new Stack<T>(capacity);
        }

        public T Pop()
        {
            lock(m_Pool)
            {
                if (this.m_Pool.Count > 0)
                    return this.m_Pool.Pop();
                return default(T);
            }
        }

        public void Push(T item)
        {
            if (item == null)
                throw new ArgumentNullException("Items added to a Pool cannot be null");

            lock(this.m_Pool)
                this.m_Pool.Push(item);
        }
    }
}
