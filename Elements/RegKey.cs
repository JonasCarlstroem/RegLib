using Microsoft.Win32;
using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegLib.Elements
{
    public class RegKey : IRegKey
    {
        private protected readonly RegistryKey _key;
        private readonly RegPath _path;

        public string Hive => _path.StartNode;
        public string Name => _path.EndNode;
        public string FullPath => _key.Name;

        public int SubKeyCount => _key.SubKeyCount;

        public virtual RegKeyCollection SubKeys => GetSubKeys();

        public int RegValueCount => _key.ValueCount;

        public RegValueCollection RegValues => GetRegValues();

        public RegPath Path => _path;

        public RegKey(RegistryKey key)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _path = new RegPath(key.Name.Split('\\'));
        }

        public RegKey GetSubKey(params string[] paths)
        {
            RegistryKey current = _key;

            foreach (var segment in paths)
            {
                if (current == null) return null;
                current = current.OpenSubKey(segment);
            }

            if (current == null) return null;

            return current;
        }

        public virtual RegValue GetValue(string name, bool writable = false) 
            => string.IsNullOrEmpty(name) 
            ? null 
            : new RegValue(
                name,
                _key.GetValue,
                _key.GetValueKind,
                writable ? _key.SetValue : (Action<string, object>)null,
                writable ? _key.DeleteValue : (Action<string>)null
            );

        protected virtual RegKeyCollection GetSubKeys()
        {
            RegKeyCollection keys = new RegKeyCollection();

            foreach (var name in _key.GetSubKeyNames())
            {
                var sub = _key.OpenSubKey(name, false);
                if (sub != null)
                    keys.Add(new RegKey(sub));
            }

            return keys;
        }

        protected virtual RegValueCollection GetRegValues()
        {
            RegValueCollection values = new RegValueCollection();

            foreach (var valueName in _key.GetValueNames())
            {
                var value = _key.GetValue(valueName);
                var kind = _key.GetValueKind(valueName);
                values.Add(
                    new RegValue(
                        valueName,
                        _key.GetValue,
                        _key.GetValueKind
                    )
                );
            }

            return values;
        }

        public void Dispose()
        {
            _key.Dispose();
        }

        public static implicit operator RegKey(RegistryKey key) => new RegKey(key);
        public static implicit operator RegistryKey(RegKey key) => key._key;
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
