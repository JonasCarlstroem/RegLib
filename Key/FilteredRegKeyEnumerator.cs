using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Key
{
    public class FilteredRegKeyEnumerator : FilteredRegBaseEnumerator<RegKey>
    {
        public FilteredRegKeyEnumerator(RegKeyCollection source, List<int> filteredIndices)
            : base(source, filteredIndices) { }
    }
}
