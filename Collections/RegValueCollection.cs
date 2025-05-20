using RegLib.Collections.Base;
using RegLib.Elements;
using RegLib.Prototypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public interface IRegValueCollection : ICollection<IRegValue>
    {

    }

    public partial class RegValueCollection : RegBaseCollection<RegValue>, IRegValueCollection
    {
        public RegValueCollection()
            : base() { }

        public RegValueCollection(IEnumerable<RegValue> values) 
            : base(values) { }

        public FilteredRegValueCollection Filter(Func<RegValue, bool> predicate)
        {
            return new FilteredRegValueCollection(this, predicate);
        }

        public override IEnumerator<RegValue> GetEnumerator()
        {
            return new RegValueEnumerator(_items, Count);
        }

        public class RegValueEnumerator : RegBaseEnumerator<RegValue>
        {
            public RegValueEnumerator(RegValue[] items, int count)
                : base(items, count) { }
        }
    }
}
