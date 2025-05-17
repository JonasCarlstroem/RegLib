using Microsoft.Win32;
using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Prototypes
{
    public class ReadOnlyRegValue : IRegElement
    {
        private readonly Func<object> _getValue;
        private readonly Func<RegistryValueKind> _getKind;

        public string Name { get; }
        public object Value => _getValue();
        public RegistryValueKind Kind => _getKind();

        internal ReadOnlyRegValue(
            string name,
            Func<string, object> getValue,
            Func<string, RegistryValueKind> getKind)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
            if (getValue == null) throw new ArgumentNullException(nameof(getValue));
            if (getKind == null) throw new ArgumentNullException(nameof(getKind));

            _getValue = () => getValue(name);
            _getKind = () => getKind(name);
        }
    }
}
