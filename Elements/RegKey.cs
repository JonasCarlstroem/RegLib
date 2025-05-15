using Microsoft.Win32;
using RegLib.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RegLib.Elements
{
    public abstract class RegKey : IRegElement, IDisposable
    {
        private static readonly Dictionary<Type, ConstructorInfo> _ctorCache = new Dictionary<Type, ConstructorInfo>();
        private protected readonly RegistryKey _key;
        private readonly RegPath _path;

        public string Hive => _path.StartNode;
        public string Name => _path.EndNode;
        public string FullPath => _key.Name;

        public int SubKeyCount => _key.SubKeyCount;

        public RegKeyCollection SubKeys => GetSubKeys();

        public int ValueCount => _key.ValueCount;

        public RegValueCollection RegValues => GetRegValues();

        public RegPath Path => _path;

        public RegKey(RegistryKey key)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _path = new RegPath(key.Name.Split('\\'));
        }

        private T GetKey<T>(Func<RegistryKey, T> factory, params string[] paths)
            where T : RegKey
        {
            RegistryKey current = _key;

            foreach(var segment in paths)
            {
                if (current == null) return null;
                current = current.OpenSubKey(segment);
            }

            if (current == null) return null;

            return factory(current);
        }

        public RegKey GetSubKey(params string[] paths)
        {
            return GetKey(key => new ReadOnlyRegKey(key), paths);
        }

        public WritableRegKey GetWritableSubKey(params string[] paths)
        {
            return GetKey(key => new WritableRegKey(key), paths);
        }

        public RegKey FindSubKeyByName(string name)
        {
            return SubKeys.FirstOrDefault(k => k.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public RegKey FindSubKeyByValueName(string valueName)
        {
            foreach (var key in SubKeys)
            {
                if (key.RegValues.Any(x => x.Name == valueName)) return key;
            }

            return null;
        }

        public RegKeyCollection FindSubKeysByValueName(string valueName)
        {
            RegKeyCollection keys = new RegKeyCollection();

            foreach(var key in SubKeys)
            {
                if (key.RegValues.Any(rv => rv.Name.Equals(valueName, StringComparison.OrdinalIgnoreCase)))
                    keys.Add(key);
            }

            return keys;
        }

        public virtual object GetValue(string name) => _key.GetValue(name);

        protected abstract RegKeyCollection GetSubKeys();

        protected virtual RegValueCollection GetRegValues()
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

        public void Dispose()
        {
            _key.Dispose();
        }

        public static implicit operator RegKey(RegistryKey key) => new ReadOnlyRegKey(key);
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
