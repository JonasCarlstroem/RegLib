using Microsoft.Win32;
using RegLib.Collections;
using System;

namespace RegLib.Elements
{
    public interface IRegElement
    {
        string Name { get; }
    }

    public interface IRegKey : IRegElement, IDisposable
    {
        string Hive { get; }
        string FullPath { get; }
        int SubKeyCount { get; }
        int RegValueCount { get; }

        IRegKeyCollection SubKeys { get; }
        IRegValueCollection RegValues { get; }

        IRegKey GetSubKey(params string[] paths);
        IRegValue GetValue(string name, bool writable = false);
    }

    public interface IRegValue : IRegElement
    {
        object Value { get; }
        RegistryValueKind Kind { get; }
        bool IsDefault { get; }

        IRegKey Owner { get; }

        void SetValue(object obj);
        void DeleteValue();
    }
}
