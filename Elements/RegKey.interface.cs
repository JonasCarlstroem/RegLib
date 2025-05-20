using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public partial class RegKey
    {
        IRegKey IRegKey.GetSubKey(params string[] paths)
            => GetSubKey(paths);

        IRegValue IRegKey.GetValue(string name, bool writable)
            => GetValue(name, writable);

        IRegKey IRegKey.CreateSubKey(string subkey, bool writable)
            => CreateSubKey(subkey, writable);

        void IRegKey.DeleteSubKey(string subkey)
            => DeleteSubKey(subkey);

        void IRegKey.DeleteSubKeyTree(string subkey)
            => DeleteSubKeyTree(subkey);
    }
}
