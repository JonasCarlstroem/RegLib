using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Value
{
    public class FilteredRegValueEnumerator : FilteredRegBaseEnumerator<RegValue>
    {
        public FilteredRegValueEnumerator(RegValueCollection source, List<int> filteredIndices)
            : base(source, filteredIndices) { }
    }
}
