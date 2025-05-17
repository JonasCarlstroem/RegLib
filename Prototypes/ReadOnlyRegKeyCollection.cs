using RegLib.Collections;
using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Prototypes
{
    public class ReadOnlyRegKeyCollection : RegKeyCollection
    {
        public ReadOnlyRegKeyCollection()
            : base() { }

        public ReadOnlyRegKeyCollection(IEnumerable<RegKey> keys)
            : base(keys) { }
    }
}
