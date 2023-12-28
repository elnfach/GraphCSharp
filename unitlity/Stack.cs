using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphC_.unitlity
{
    internal class Stack<T>
    {
        private T[] items;
        private int count;
        const int n = 10;
        public Stack()
        {
            items = new T[n];
        }
        public Stack(int length)
        {
            items = new T[length];
        }

        public bool IsEmpty
        {
            get
            {
                return count == 0;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }
        public void Push(T item)
        {
            if (count == items.Length)
                throw new InvalidOperationException( "Переполнение стека" );
            items[count++] = item;
        }
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException( "Стек пуст" );
            T item = items[--count];
            items[count] = default( T );
            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException( "Стек пуст" );
            return items[count - 1];
        }
    }
}
