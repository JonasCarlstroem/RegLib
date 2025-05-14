using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Elements
{
    public interface IRegElement
    {
        string Name { get; }
    }

    public interface IReadOnlyRegElement : IRegElement
    {

    }

    public interface IWritableRegElement : IRegElement
    {

    }
}
