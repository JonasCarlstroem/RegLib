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
    public class RegKeyCollection : RegBaseCollection<RegKey>
    {
        public RegKeyCollection()
            : base() { }

        public RegKeyCollection(IEnumerable<RegKey> keys)
            : base(keys) { }

        public FilteredRegKeyCollection Filter(Func<RegKey, bool> predicate)
        {
            return new FilteredRegKeyCollection(this, predicate);
        }

        public FilteredRegKeyCollection Where(Func<RegKey, bool> predicate)
        {
            return Filter(predicate);
        }

        public override IEnumerator<RegKey> GetEnumerator()
        {
            return new RegKeyEnumerator(_items, Count);
        }

        internal class RegKeyEnumerator : RegBaseEnumerator<RegKey>
        {
            public RegKeyEnumerator(RegKey[] items, int count)
                : base(items, count) { }
        }
    }
}
