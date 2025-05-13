using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Key
{
    public class RegKeyCollection : RegBaseCollection<RegKey>
    {
        public RegKeyCollection()
            : base() { }

        public RegKeyCollection(IEnumerable<RegKey> keys)
            : base(keys) { }

        public override IEnumerator<RegKey> GetEnumerator()
        {
            return new RegKeyEnumerator(_items, Count);
        }
    }

    //public class RegKeyCollection : ICollection<RegKey>
    //{
    //    private RegKey[] _keys = Array.Empty<RegKey>();
    //    private int _count = 0;
    //    private int _capacity => _keys?.Length ?? 0;

    //    public int Count => _count;
    //    public int Capacity => _capacity;
    //    public bool IsReadOnly => false;

    //    public RegKey this[int index]
    //    {
    //        get { return null; }
    //        set { }
    //    }

    //    public RegKeyCollection()
    //    {
    //        _count = 0;
    //    }

    //    public RegKeyCollection(IEnumerable<RegKey> keys)
    //    {
    //        foreach (var key in keys)
    //        {
    //            Add(key);
    //        }
    //    }

    //    public void Add(RegKey item)
    //    {
    //        if (_count == _capacity)
    //        {
    //            int capacity = _capacity == 0 ? 1 : _capacity * 2;
    //            RegKey[] temp = new RegKey[capacity];

    //            Array.Copy(_keys, temp, _count);

    //            _keys = temp;
    //        }

    //        _keys[_count++] = item;
    //    }

    //    public FilteredRegKeyCollection Filter(Func<RegKey, bool> predicate)
    //    {
    //        return new FilteredRegKeyCollection(this, predicate);
    //    }

    //    public FilteredRegKeyCollection Where(Func<RegKey, bool> predicate)
    //    {
    //        return new FilteredRegKeyCollection(this, predicate);
    //    }

    //    public void Clear() => _count = 0;

    //    public bool Contains(RegKey item)
    //    {
    //        var comparer = EqualityComparer<RegKey>.Default;

    //        for (int i = 0; i < _count; i++)
    //        {
    //            if (comparer.Equals(_keys[i], item)) return true;
    //        }

    //        return false;
    //    }

    //    public void CopyTo(RegKey[] array, int arrayIndex)
    //    {
    //        if (array == null)
    //            throw new ArgumentNullException(nameof(array));

    //        if (arrayIndex < 0)
    //            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index cannot be negative.");

    //        if (array.Length - arrayIndex < _count)
    //            throw new ArgumentException("The destination array is not large enough to copy all the elements.");

    //        for (int i = 0; i < _count; i++)
    //        {
    //            array[arrayIndex + i] = _keys[i];
    //        }
    //    }

    //    public bool Remove(RegKey item)
    //    {
    //        var comparer = EqualityComparer<RegKey>.Default;

    //        for (int i = 0; i < _count; i++)
    //        {
    //            if (comparer.Equals(_keys[i], item))
    //            {
    //                for (int j = i; j < _count - 1; j++)
    //                {
    //                    _keys[j] = _keys[j + 1];
    //                }

    //                _keys[_count - 1] = default;
    //                _count--;

    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    public IEnumerator<RegKey> GetEnumerator()
    //    {
    //        return new RegKeyEnumerator(_keys, _count);
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    private struct RegKeyEnumerator : IEnumerator<RegKey>
    //    {
    //        private readonly RegKey[] _items;
    //        private readonly int _count;
    //        private int _index;
    //        private readonly Func<RegKey, bool> _predicate;

    //        public RegKeyEnumerator(RegKey[] items, int count)
    //        {
    //            _items = items;
    //            _count = count;
    //            _predicate = null;
    //            _index = -1;
    //        }

    //        public RegKeyEnumerator(RegKey[] items, int count, Func<RegKey, bool> predicate)
    //        {
    //            _items = items;
    //            _count = count;
    //            _predicate = predicate;
    //            _index = -1;
    //        }

    //        public RegKey Current => _items[_index];
    //        object IEnumerator.Current => Current;

    //        public bool MoveNext() => ++_index < _count;
    //        //{
    //        //	while(++_index < _count) 
    //        //	{
    //        //		if(_predicate == null || _predicate(_items[_index])) return true;
    //        //	}
    //        //	
    //        //	return false;
    //        //}

    //        public void Reset() => _index = -1;
    //        public void Dispose() { }
    //    }
    //}
}
