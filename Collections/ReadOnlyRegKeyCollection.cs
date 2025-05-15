using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public class ReadOnlyRegKeyCollection : RegKeyCollection<ReadOnlyRegKey>
    {
        public ReadOnlyRegKeyCollection() 
            : base() { }

        public ReadOnlyRegKeyCollection(IEnumerable<ReadOnlyRegKey> keys)
            : base(keys) { }
    }
}
