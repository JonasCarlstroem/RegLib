using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib
{
    public abstract class RegBaseEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _items;
        private readonly int _count;
        private int _index;

        public RegBaseEnumerator(T[] items, int count)
        {
            _items = items;
            _count = count;
            _index = -1;
        }

        public T Current => _items[_index];
        object IEnumerator.Current => Current;

        public virtual bool MoveNext() => ++_index < _count;
        public virtual void Reset() => _index = -1;
        public virtual void Dispose() { }
    }
}
