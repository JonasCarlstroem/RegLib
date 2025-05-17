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
    public class FilteredRegKeyCollection : FilteredRegBaseCollection<RegKey, RegKeyCollection>
    {
        public FilteredRegKeyCollection(RegKeyCollection source, Func<RegKey, bool> predicate)
            : base(source, predicate) { }

        public override IEnumerator<RegKey> GetEnumerator()
        {
            return new FilteredRegKeyEnumerator(_source, _filteredIndices);
        }

        internal class FilteredRegKeyEnumerator : FilteredRegBaseEnumerator<RegKey>
        {
            public FilteredRegKeyEnumerator(RegKeyCollection source, List<int> filteredIndices)
                : base(source, filteredIndices) { }
        }
    }
}
