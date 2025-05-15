using RegLib.Collections.Base;
using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public class FilteredRegValueCollection : FilteredRegBaseCollection<ReadOnlyRegValue, RegValueCollection>
    {
        public FilteredRegValueCollection(RegValueCollection source, Func<ReadOnlyRegValue, bool> predicate)
            : base(source, predicate) { }

        public override IEnumerator<ReadOnlyRegValue> GetEnumerator()
        {
            return new FilteredRegValueEnumerator(_source, _filteredIndices);
        }

        public class FilteredRegValueEnumerator : FilteredRegBaseEnumerator<ReadOnlyRegValue>
        {
            public FilteredRegValueEnumerator(RegValueCollection source, List<int> filteredIndices)
                : base(source, filteredIndices) { }
        }
    }
}
