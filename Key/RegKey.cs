using Microsoft.Win32;
using RegLib.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Key
{
    public class RegKey
    {
        private readonly RegistryKey _key;

        public RegKeyCollection SubRegKeys
        {
            get
            {
                string[] subKeys = _key.GetSubKeyNames();
                RegKeyCollection keys = new RegKeyCollection();

                foreach (var subKey in subKeys)
                {
                    keys.Add(_key.OpenSubKey(subKey));
                }

                return keys;
            }
        }

        public int ValueNameCount
        {
            get
            {
                return _key.GetValueNames().Count();
            }
        }

        public RegValueCollection RegValues
        {
            get
            {
                string[] valueNames = _key.GetValueNames();
                RegValueCollection values = new RegValueCollection();

                foreach (var valueName in valueNames)
                {
                    var value = _key.GetValue(valueName);
                    values.Add(new RegValue(valueName, value));
                }

                return values;
            }
        }

        public RegKey(RegistryKey key)
        {
            _key = key;
        }

        public RegKey GetSubKey(params string[] paths)
        {
            return paths.Aggregate(_key, (x, y) => x.OpenSubKey(y));
        }

        public RegKey FindSubKeyByValueName(string valueName)
        {
            foreach (var key in SubRegKeys)
            {
                if (key.RegValues.Any(x => x.Name == valueName)) return key;
            }

            return null;
        }

        public RegKey[] FindSubKeysByValueName(string valueName)
        {
            return SubRegKeys.Aggregate(
                new List<RegKey>(),
                (list, subKey) => {
                    if (subKey.RegValues.Any(vnp => vnp.Name == valueName)) list.Add(subKey);
                    return list;
                }).ToArray();
        }

        public static implicit operator RegKey(RegistryKey key) => new RegKey(key);
        public static implicit operator RegistryKey(RegKey key) => key._key;
    }
}
