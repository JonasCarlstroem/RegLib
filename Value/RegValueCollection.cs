using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Value
{
    public class RegValueCollection : RegBaseCollection<RegValue>
    {
        public RegValueCollection()
            : base() { }

        public RegValueCollection(IEnumerable<RegValue> values) 
            : base(values) { }

        public override IEnumerator<RegValue> GetEnumerator()
        {
            return new RegValueEnumerator(_items, Count);
        }
    }
    //public class RegValueCollection : ICollection<RegValue>
    //{
    //    private RegValue[] _values = Array.Empty<RegValue>();
    //    private int _count = 0;
    //    private int _capacity => _values?.Length ?? 0;

    //    public int Count => _count;
    //    public int Capacity => _capacity;
    //    public bool IsReadOnly => false;

    //    public RegValueCollection()
    //    {
    //        _count = 0;
    //    }

    //    public RegValueCollection(IEnumerable<RegValue> values)
    //    {
    //        foreach(var value in values)
    //        {
    //            Add(value);
    //        }
    //    }

    //    public void Add(RegValue item)
    //    {

    //    }

    //    public void Clear()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Contains(RegValue item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CopyTo(RegValue[] array, int arrayIndex)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerator<RegValue> GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Remove(RegValue item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
