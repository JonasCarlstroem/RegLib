using Microsoft.Win32;
using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegLib.Elements
{
    public class RegKey : IRegElement
    {
        private readonly RegistryKey _key;
        private readonly RegPath _path;

        public string Hive => _path.StartNode;
        public string Name => _path.EndNode;
        public string FullPath => _key.Name;

        public int SubKeyCount => _key.SubKeyCount;

        public RegKeyCollection SubKeys
        {
            get
            {
                RegKeyCollection keys = new RegKeyCollection();

                foreach (var name in _key.GetSubKeyNames())
                {
                    var sub = _key.OpenSubKey(name);
                    if (sub != null)
                        keys.Add(sub);
                }

                return keys;
            }
        }

        public int ValueCount => _key.ValueCount;

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

        public RegPath Path => _path;

        public RegKey(RegistryKey key)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _path = new RegPath(key.Name.Split('\\'));
        }

        public RegKey GetSubKey(params string[] paths)
        {
            RegistryKey current = _key;

            foreach(var segment in paths)
            {
                if (current == null) return null;
                current = current.OpenSubKey(segment);
            }

            return current == null ? null : new RegKey(current);
        }

        public RegKey FindSubKeyByValueName(string valueName)
        {
            foreach (var key in SubKeys)
            {
                if (key.RegValues.Any(x => x.Name == valueName)) return key;
            }

            return null;
        }

        public RegKey[] FindSubKeysByValueName(string valueName)
        {
            return SubKeys.Aggregate(
                new List<RegKey>(),
                (list, subKey) => {
                    if (subKey.RegValues.Any(vnp => vnp.Name == valueName)) list.Add(subKey);
                    return list;
                }).ToArray();
        }

        public static implicit operator RegKey(RegistryKey key) => new RegKey(key);
    }

    public readonly struct RegPath
    {
        private readonly string _fullPath;
        private readonly string[] _segments;

        public string FullPath => _fullPath;

        public IReadOnlyList<string> Segments => _segments;

        public string StartNode => _segments.First();
        public string EndNode => _segments.Last();

        public RegPath(params string[] paths)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            _segments = paths
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(p => p.Trim('\\', '/'))
                .ToArray();

            _fullPath = string.Join("\\", _segments);
        }

        public override string ToString() => _fullPath;
    }
}
