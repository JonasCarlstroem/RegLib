using RegLib.Collections.Base;
using RegLib.Elements;
using RegLib.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public class FilteredRegValueCollection : FilteredRegBaseCollection<RegValue, RegValueCollection>
    {
        public FilteredRegValueCollection(RegValueCollection source, Func<RegValue, bool> predicate)
            : base(source, predicate) { }

        public override IEnumerator<RegValue> GetEnumerator()
        {
            return new FilteredRegValueEnumerator(_source, _filteredIndices);
        }

        public class FilteredRegValueEnumerator : FilteredRegBaseEnumerator<RegValue>
        {
            public FilteredRegValueEnumerator(RegValueCollection source, List<int> filteredIndices)
                : base(source, filteredIndices) { }
        }
    }
}
