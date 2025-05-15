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
    public interface IRegKeyCollection<out T> : IEnumerable<T>
        where T : ReadOnlyRegKey
    {
        int Count { get; }
        T FindByName(string name);
        T FindByValueName(string valueName);
        T FindByValue(object value);

        ReadOnlyRegKeyCollection AsReadOnly();
        WritableRegKeyCollection AsWritable();
    }

    public class RegKeyCollection<T> : RegBaseCollection<T>, IRegKeyCollection<T>
        where T : ReadOnlyRegKey
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

        public T FindByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            foreach (var key in this)
            {
                if (string.Equals(key.Name, name, StringComparison.OrdinalIgnoreCase))
                    return key;
            }

            return null;
        }

        public T FindByValueName(string valueName)
        {
            if (string.IsNullOrEmpty(valueName)) return null;

            foreach(var key in this)
            {
                if (key.RegValues.Any(rv => rv.Name.Equals(valueName, StringComparison.OrdinalIgnoreCase))) return key;
            }

            return null;
        }

        public T FindByValue(object value)
        {
            if (value == null) return null;

            foreach(var key in this)
            {
                if (key.RegValues.Any(rv => rv.Value.Equals(value))) return key;
            }

            return null;
        }

        public ReadOnlyRegKeyCollection AsReadOnly() => new ReadOnlyRegKeyCollection(_items.Cast<ReadOnlyRegKey>());
        public WritableRegKeyCollection AsWritable() => new WritableRegKeyCollection(_items.Cast<WritableRegKey>());

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
