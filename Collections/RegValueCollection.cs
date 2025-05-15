using RegLib.Collections.Base;
using RegLib.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public interface IRegValueCollection<out T> : IEnumerable<T>
        where T : ReadOnlyRegValue
    {
        int Count { get; }

    }

    public class RegValueCollection : RegBaseCollection<ReadOnlyRegValue>
    {
        public RegValueCollection()
            : base() { }

        public RegValueCollection(IEnumerable<ReadOnlyRegValue> values) 
            : base(values) { }

        public FilteredRegValueCollection Filter(Func<ReadOnlyRegValue, bool> predicate)
        {
            return new FilteredRegValueCollection(this, predicate);
        }

        public FilteredRegValueCollection Where(Func<ReadOnlyRegValue, bool> predicate)
        {
            return Filter(predicate);
        }

        public override IEnumerator<ReadOnlyRegValue> GetEnumerator()
        {
            return new RegValueEnumerator(_items, Count);
        }

        public class RegValueEnumerator : RegBaseEnumerator<ReadOnlyRegValue>
        {
            public RegValueEnumerator(ReadOnlyRegValue[] items, int count)
                : base(items, count) { }
        }
    }
}
