using Microsoft.Win32;
using RegLib.Collections;
using RegLib.Collections.Base;
using RegLib.Elements;
using RegLib.Types;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public interface IRegKeyCollection
    {
        IRegKey FindSubKeyBy(Func<IRegKey, bool> predicate, bool recurse = false);
        IRegKeyCollection FindSubKeysBy(Func<IRegKey, bool> predicate, bool recurse = false);
        IRegKey FindSubKeyByName(string name, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKeyCollection FindSubKeysByName(string name, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKey FindSubKeyByValueName(string valueName, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKeyCollection FindSubKeysByValueName(string valueName, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKey FindSubKeyByValue(object value, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKey FindSubKeyByValue<T>(T value, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKeyCollection FindSubKeysByValue(object value, bool recurse = false, StringMatch mode = StringMatch.Exact);
        IRegKeyCollection FindSubKeysByValue<T>(T value, bool recurse = false, StringMatch mode = StringMatch.Exact);
    }

    public partial class RegKeyCollection : RegBaseCollection<RegKey>, IRegKeyCollection
    {
        public RegKeyCollection()
            : base() { }

        public RegKeyCollection(IEnumerable<RegKey> keys)
            : base(keys) { }

        public FilteredRegKeyCollection Filter(Func<RegKey, bool> predicate)
        {
            return new FilteredRegKeyCollection(this, predicate);
        }

        public RegKey FindSubKeyBy(Func<RegKey, bool> predicate, bool recurse = false)
        {
            if (predicate == null) return null;

            return recurse
                ? FindRecursive(predicate).FirstOrDefault()
                : this.FirstOrDefault(predicate);
        }

        public RegKeyCollection FindSubKeysBy(Func<RegKey, bool> predicate, bool recurse = false) 
        {
            if (predicate == null) return null;

            var matches = recurse
                ? FindRecursive(predicate)
                : this.Where(predicate);

            return new RegKeyCollection(matches);
        }

        public RegKey FindSubKeyByName(
            string name, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return FindSubKeyBy(key => 
                Match(key.Name, name, mode), 
                recurse);
        }

        public RegKeyCollection FindSubKeysByName(
            string name, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (string.IsNullOrEmpty(name)) return null;

            return FindSubKeysBy(key => 
                Match(key.Name, name, mode), 
                recurse);
        }

        public RegKey FindSubKeyByValueName(
            string valueName, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (string.IsNullOrEmpty(valueName)) return null;

            return FindSubKeyBy(key => 
                key.RegValues.Any(rv => 
                    Match(rv.Name, valueName, mode)), 
                    recurse);
        }

        public RegKeyCollection FindSubKeysByValueName(
            string valueName, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (string.IsNullOrEmpty(valueName)) return null;

            return FindSubKeysBy(key => 
                key.RegValues.Any(rv => 
                    Match(rv.Name, valueName, mode)), 
                    recurse);
        }

        public RegKey FindSubKeyByValue(
            object value, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (value == null) return null;

            if (!TryMapTypeToKind(value.GetType(), out var expectedKind))
                throw new NotSupportedException($"Unsupported value type: {value.GetType()}");

            return FindSubKeyBy(key => 
                key.RegValues.Any(rv => 
                    rv.Kind == expectedKind &&
                    rv.Value is object val &&
                    MatchValue(val, value, mode)), 
                    recurse);
        }

        public RegKey FindSubKeyByValue<T>(
            T value,
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (!TryMapTypeToKind(typeof(T), out var expectedKind))
                throw new NotSupportedException($"Unsupported value type: {typeof(T).Name}");

            return FindSubKeyBy(key =>
                key.RegValues.Any(rv => 
                    rv.Kind == expectedKind &&
                    rv.Value is T val &&
                    MatchValue(val, value, mode)),
                    recurse);
        }

        public RegKeyCollection FindSubKeysByValue(
            object value, 
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (value == null) return null;

            if (!TryMapTypeToKind(value.GetType(), out var expectedKind))
                throw new NotSupportedException($"Unsupported value type: {value.GetType()}");

            return FindSubKeysBy(key => 
                key.RegValues.Any(rv => 
                    rv.Kind == expectedKind &&
                    rv.Value is object val &&
                    MatchValue(val, value, mode)), 
                    recurse);
        }

        public RegKeyCollection FindSubKeysByValue<T>(
            T value,
            bool recurse = false,
            StringMatch mode = StringMatch.Exact)
        {
            if (!TryMapTypeToKind(typeof(T), out var expectedKind))
                throw new NotSupportedException($"Unsupported value type: {typeof(T).Name}");

            return FindSubKeysBy(key =>
                key.RegValues.Any(rv =>
                    rv.Kind == expectedKind &&
                    rv.Value is T val &&
                    MatchValue(val, value, mode)),
                    recurse);
        }

        private IEnumerable<RegKey> FindRecursive(Func<RegKey, bool> predicate)
        {
            foreach (var key in this)
            {
                if (predicate(key))
                    yield return key;

                foreach (var subKey in key.SubKeys.FindRecursive(predicate))
                    yield return subKey;
            }
        }

        private bool Match(string input, string target, StringMatch mode)
        {
            switch(mode)
            {
                case StringMatch.Exact:
                    return string.Equals(input, target, StringComparison.OrdinalIgnoreCase);
                case StringMatch.Contains:
                    return input?.IndexOf(target, StringComparison.OrdinalIgnoreCase) >= 0;
                case StringMatch.StartsWith:
                    return input?.StartsWith(target, StringComparison.OrdinalIgnoreCase) ?? false;
                case StringMatch.EndsWith:
                    return input?.EndsWith(target, StringComparison.OrdinalIgnoreCase) ?? false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), $"Unknown match mode: {mode}");
            }
        }

        private bool MatchValue<T>(T actual, T expected, StringMatch mode)
        {
            if (typeof(T) != typeof(string))
                return Equals(actual, expected);

            var a = actual as string;
            var b = expected as string;

            return Match(a, b, mode);
        }

        private static bool TryMapTypeToKind(Type type, out RegistryValueKind kind)
        {
            if(type == typeof(string)) { kind = RegistryValueKind.String; return true; }
            if(type == typeof(int)) { kind = RegistryValueKind.DWord; return true; }
            if(type == typeof(long)) { kind = RegistryValueKind.QWord; return true; }
            if(type == typeof(byte[])) { kind = RegistryValueKind.Binary; return true; }
            if(type == typeof(string[])) { kind = RegistryValueKind.MultiString; return true; }
            kind = RegistryValueKind.Unknown; return false;
        }

        public override IEnumerator<RegKey> GetEnumerator()
        {
            return new RegKeyEnumerator(_items, Count);
        }

        internal class RegKeyEnumerator : RegBaseEnumerator<RegKey>
        {
            public RegKeyEnumerator(RegKey[] items, int count)
                : base(items, count) { }
        }
    }
}