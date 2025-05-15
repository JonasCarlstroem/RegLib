using Microsoft.Win32;
using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public class ReadOnlyRegKey : RegKey
    {
        public ReadOnlyRegKey(RegistryKey key)
            : base(key) {}

        protected override RegKeyCollection GetSubKeys()
        {
            RegKeyCollection keys = new RegKeyCollection();

            foreach(var name in _key.GetSubKeyNames())
            {
                var sub = _key.OpenSubKey(name, false);
                if (sub != null)
                    keys.Add(new ReadOnlyRegKey(sub));
            }

            return keys;
        }
    }
}
