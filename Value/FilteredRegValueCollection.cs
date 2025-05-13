using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Value
{
    public class FilteredRegValueCollection : FilteredRegBaseCollection<RegValue>
    {
        public FilteredRegValueCollection(RegValueCollection source, Func<RegValue, bool> predicate)
            : base(source, predicate) { }
    }
}
