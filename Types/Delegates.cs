using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Values
{
    delegate object GetValue();
    delegate RegistryValueKind GetKind();
    delegate void SetValue(object obj);
    delegate void DeleteValue();
}
