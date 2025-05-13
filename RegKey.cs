using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib
{
    public class RegKey
    {
        private readonly RegistryKey _key;

        public RegKeyCollection SubKeys
        {
            get
            {
                string[] subKeys = _key.GetSubKeyNames();
                List<RegKey> keys = new List<RegKey>();

                foreach (var subKey in subKeys)
                {
                    keys.Add(_key.OpenSubKey(subKey));
                }

                return new RegKeyCollection(keys.AsEnumerable());
            }
        }

        public int ValueNameCount
        {
            get
            {
                return _key.GetValueNames().Count();
            }
        }

        public KeyValuePair<string, object>[] ValueNamePairs
        {
            get
            {
                string[] valueNames = _key.GetValueNames();
                List<KeyValuePair<string, object>> kvps = new List<KeyValuePair<string, object>>();

                foreach (var valueName in valueNames)
                {
                    var value = _key.GetValue(valueName);
                    kvps.Add(new KeyValuePair<string, object>(valueName, value));
                }

                return kvps.ToArray();
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
            foreach (var key in SubKeys)
            {
                if (key.ValueNamePairs.Any(x => x.Key == valueName)) return key;
            }

            return null;
        }

        public RegKey[] FindSubKeysByValueName(string valueName)
        {
            return SubKeys.Aggregate(
                new List<RegKey>(),
                (list, subKey) => {
                    if (subKey.ValueNamePairs.Any(vnp => vnp.Key == valueName)) list.Add(subKey);
                    return list;
                }).ToArray();
        }

        public static implicit operator RegKey(RegistryKey key) => new RegKey(key);
        public static implicit operator RegistryKey(RegKey key) => key._key;
    }
}
