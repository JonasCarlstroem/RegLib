using Microsoft.Win32;
using RegLib.Values;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public partial class RegValue : IRegValue
    {
        private readonly GetValue _getValue;
        private readonly GetKind _getKind;
        private readonly SetValue _setValue;
        private readonly DeleteValue _deleteValue;

        public string Name { get; }
        public object Value => _getValue();
        public RegistryValueKind Kind => _getKind();
        public bool IsDefault => Name == "";

        public RegKey Owner { get; }

        internal RegValue(
            RegKey owner,
            string name,
            Func<string, object> getValue,
            Func<string, RegistryValueKind> getKind,
            Action<string, object> setValue = null,
            Action<string> deleteValue = null)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (getValue == null) throw new ArgumentNullException(nameof(getValue));
            if (getKind == null) throw new ArgumentNullException(nameof(getKind));

            Owner = owner;
            Name = name;
            _getValue = () => getValue(name);
            _getKind = () => getKind(name);

            if (setValue != null)
            {
                _setValue = (obj) => setValue(name, obj);
            }

            if (deleteValue != null)
            {
                _deleteValue = () => deleteValue(name);
            }
        }

        public void SetValue(object obj)
        {
            if (_setValue == null) return;
            _setValue(obj);
        }

        public void DeleteValue()
        {
            if (_deleteValue == null) return;
            _deleteValue();
        }
    }
}
