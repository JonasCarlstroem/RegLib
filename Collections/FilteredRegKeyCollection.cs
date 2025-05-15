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
    public class FilteredRegKeyCollection<T> : FilteredRegBaseCollection<T, RegKeyCollection<T>>
        where T : RegKey
    {
        public FilteredRegKeyCollection(RegKeyCollection<T> source, Func<T, bool> predicate)
            : base(source, predicate) { }

        public override IEnumerator<T> GetEnumerator()
        {
            return new FilteredRegKeyEnumerator<T>(_source, _filteredIndices);
        }

        internal class FilteredRegKeyEnumerator<U> : FilteredRegBaseEnumerator<U>
            where U : T
        {
            public FilteredRegKeyEnumerator(RegKeyCollection<U> source, List<int> filteredIndices)
                : base(source, filteredIndices) { }
        }
    }
}
