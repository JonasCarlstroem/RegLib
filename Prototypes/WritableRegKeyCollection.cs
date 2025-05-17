using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Prototypes
{
    public class WritableRegKeyCollection : RegKeyCollection
    {
        public WritableRegKeyCollection()
            : base() { }

        public WritableRegKeyCollection(IEnumerable<WritableRegKey> keys)
            : base(keys) { }
    }
}
