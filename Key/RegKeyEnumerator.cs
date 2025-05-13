using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Key
{
    public class RegKeyEnumerator : RegBaseEnumerator<RegKey>
    {
        public RegKeyEnumerator(RegKey[] items, int count)
            : base(items, count) { }
    }
}
