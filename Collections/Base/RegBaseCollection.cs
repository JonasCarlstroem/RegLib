using RegLib.Elements;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RegLib.Collections.Base
{
    public abstract class RegBaseCollection<T> : ICollection<T>
        where T : IRegElement
    {
        private protected T[] _items = Array.Empty<T>();
        private int _count = 0;
        private int _capacity => _items?.Length ?? 0;

        public int Count => _count;
        public int Capacity => _capacity;
        public bool IsReadOnly => false;

        public virtual T this[int index]
        {
            get 
            {
                if (index < 0 || index > _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _items[index];
            }

            set 
            {
                if (index < 0 || index > _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                _items[index] = value;
            }
        }

        protected RegBaseCollection()
        {
            _count = 0;
        }

        protected RegBaseCollection(IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }

        public virtual void Add(T item)
        {
            if(_count == _capacity)
            {
                int capacity = _capacity == 0 ? 1 : _capacity * 2;
                T[] temp = new T[capacity];

                Array.Copy(_items, temp, _count);

                _items = temp;
            }

            _items[_count++] = item;
        }

        public virtual void Clear() => _count = 0;

        public virtual bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for(int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_items[i], item)) return true;
            }

            return false;
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index cannot be negative.");

            if (array.Length - arrayIndex < _count)
                throw new ArgumentException("The destination array is not large enough to copy all the elements.");

            for(int i = 0; i < _count; i++)
            {
                array[arrayIndex + i] = _items[i];
            }
        }

        public virtual bool Remove(T item)
        {
            var comparer = EqualityComparer<T>.Default;

            for(int i = 0; i < _count; i++)
            {
                if (comparer.Equals(_items[i], item))
                {
                    for(int j = i; j < _count - 1; j++)
                    {
                        _items[j] = _items[j + 1];
                    }

                    _items[_count - 1] = default;
                    _count--;

                    return true;
                }
            }

            return false;
        }

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    //public abstract class BaseCollectionEnumerator<T> : IEnumerator<T>
    //{
    //    private readonly T[] _items;
    //    private readonly int _count;
    //    private int _index;
    //    private readonly Func<T, bool> _predicate;

    //    public BaseCollectionEnumerator(T[] items, int count)
    //    {
    //        _items = items;
    //        _count = count;
    //        _predicate = null;
    //        _index = -1;
    //    }

    //    public BaseCollectionEnumerator(T[] items, int count, Func<T, bool> predicate)
    //    {
    //        _items = items;
    //        _count = count;
    //        _predicate = predicate;
    //        _index = -1;
    //    }

    //    public T Current => _items[_index];
    //    object IEnumerator.Current => Current;

    //    public virtual bool MoveNext() => ++_index < _count;
    //    public virtual void Reset() => _index = -1;
    //    public virtual void Dispose() { }
    //}
}
