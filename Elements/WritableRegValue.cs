using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public class WritableRegValue : ReadOnlyRegValue
    {
        private readonly Action<object> _setValue;
        private readonly Action _deleteValue;

        public WritableRegValue(
            string name,
            Func<string, object> getValue,
            Func<string, RegistryValueKind> getKind,
            Action<string, object> setValue,
            Action<string> deleteValue) 
            : base(name, getValue, getKind) 
        {
            _setValue = (object obj) => setValue(name, obj);
            _deleteValue = () => deleteValue(name);
        }

        public void SetValue(object obj) => _setValue(obj);
        public void DeleteValue() => _deleteValue();
    }
}
