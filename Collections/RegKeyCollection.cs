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
    public class RegKeyCollection<T> : RegBaseCollection<T>
        where T : RegKey
    {
        public RegKeyCollection()
            : base() { }

        public RegKeyCollection(IEnumerable<T> keys)
            : base(keys) { }

        public FilteredRegKeyCollection<T> Filter(Func<T, bool> predicate)
        {
            return new FilteredRegKeyCollection<T>(this, predicate);
        }

        public FilteredRegKeyCollection<T> Where(Func<T, bool> predicate)
        {
            return Filter(predicate);
        }

        public T FindKeyByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            foreach (var key in this)
            {
                if (string.Equals(key.Name, name, StringComparison.OrdinalIgnoreCase))
                    return key;
            }

            return null;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new RegKeyEnumerator(_items, Count);
        }

        internal class RegKeyEnumerator : RegBaseEnumerator<T>
        {
            public RegKeyEnumerator(T[] items, int count)
                : base(items, count) { }
        }
    }
}
