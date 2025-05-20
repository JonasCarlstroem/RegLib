using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public partial class RegValue
    {
        IRegKey IRegValue.Owner => Owner;
    }
}
