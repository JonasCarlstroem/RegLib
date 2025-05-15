using Microsoft.Win32;
using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public class WritableRegKey : RegKey
    {
        public WritableRegKey(RegistryKey key) 
            : base(key)
        {
            if(!key.GetAccessControl().AreAccessRulesProtected)
            {
                
            }
        }

        public void SetValue(string name, object value, RegistryValueKind kind = RegistryValueKind.Unknown)
        {
            _key.SetValue(name, value, kind);
        }

        public void DeleteValue(string name, bool throwOnMissing = true)
        {
            _key.DeleteValue(name, throwOnMissing);
        }

        public void DeleteSubKey(string name, bool throwOnMissing = true)
        {
            _key.DeleteSubKey(name, throwOnMissing);
        }

        public WritableRegKey CreateSubKey(string name)
        {
            var newKey = _key.CreateSubKey(name);
            return newKey != null ? new WritableRegKey(newKey) : null;
        }

        protected override RegKeyCollection GetSubKeys()
        {
            RegKeyCollection keys = new RegKeyCollection();

            foreach(var name in _key.GetSubKeyNames())
            {
                var sub = _key.OpenSubKey(name, true);
                if (sub != null)
                    keys.Add(new WritableRegKey(sub));
            }

            return keys;
        }
    }
}
