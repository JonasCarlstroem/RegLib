using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Value
{
    public class RegValueEnumerator : RegBaseEnumerator<RegValue>
    {
        public RegValueEnumerator(RegValue[] items, int count)
            : base(items, count) { }
    }
}
