using RegLib.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public partial class RegValueCollection
    {
        //void ICollection<IRegValue>.Add(IRegValue item)
        //{
        //    if (item is RegValue rv)
        //        Add(rv);
        //    else
        //        throw new ArgumentException("Only RegValue instances are supported.");
        //}

        //bool ICollection<IRegValue>.Contains(IRegValue item)
        //    => item is RegValue rv && Contains(rv);

        //void ICollection<IRegValue>.CopyTo(IRegValue[] array, int arrayIndex)
        //{
        //    if (array == null) throw new ArgumentNullException(nameof(array));
        //    if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        //    if (array.Length - arrayIndex < Count) throw new ArgumentException("Not enough space in array.");

        //    for (int i = 0; i < Count; i++)
        //        array[arrayIndex + i] = this[i];
        //}

        //bool ICollection<IRegValue>.Remove(IRegValue item)
        //    => item is RegValue rv && Remove(rv);

        //IEnumerator<IRegValue> IEnumerable<IRegValue>.GetEnumerator()
        //{
        //    foreach (var value in this)
        //        yield return value;
        //}
    }
}
