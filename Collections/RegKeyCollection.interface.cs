using RegLib.Elements;
using RegLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Collections
{
    public partial class RegKeyCollection
    {
        void ICollection<IRegKey>.Add(IRegKey item)
        {
            if (item is RegKey rk)
                Add(rk);
            else
                throw new ArgumentException("Only RegKey instances are supported.");
        }

        bool ICollection<IRegKey>.Contains(IRegKey item)
            => item is RegKey rk && Contains(rk);

        void ICollection<IRegKey>.CopyTo(IRegKey[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count) throw new ArgumentException("Not enough space in array.");

            for (int i = 0; i < Count; i++)
                array[arrayIndex + i] = this[i];
        }

        bool ICollection<IRegKey>.Remove(IRegKey item)
            => item is RegKey rk && Remove(rk);

        IEnumerator<IRegKey> IEnumerable<IRegKey>.GetEnumerator()
        {
            foreach (var key in this)
                yield return key;
        }

        IRegKey IRegKeyCollection.FindSubKeyBy(Func<IRegKey, bool> predicate, bool recurse)
            => FindSubKeyBy(predicate, recurse);

        IRegKeyCollection IRegKeyCollection.FindSubKeysBy(Func<IRegKey, bool> predicate, bool recurse)
            => FindSubKeysBy(predicate, recurse);

        IRegKey IRegKeyCollection.FindSubKeyByName(string name, bool recurse, StringMatch mode)
            => FindSubKeyByName(name, recurse, mode);

        IRegKeyCollection IRegKeyCollection.FindSubKeysByName(string name, bool recurse, StringMatch mode)
            => FindSubKeysByName(name, recurse, mode);

        IRegKey IRegKeyCollection.FindSubKeyByValueName(string valueName, bool recurse, StringMatch mode)
            => FindSubKeyByValueName(valueName, recurse, mode);

        IRegKeyCollection IRegKeyCollection.FindSubKeysByValueName(string valueName, bool recurse, StringMatch mode)
            => FindSubKeysByValueName(valueName, recurse, mode);

        IRegKey IRegKeyCollection.FindSubKeyByValue(object value, bool recurse, StringMatch mode)
            => FindSubKeyByValue(value, recurse, mode);

        IRegKey IRegKeyCollection.FindSubKeyByValue<T>(T value, bool recurse, StringMatch mode)
            => FindSubKeyByValue(value, recurse, mode);

        IRegKeyCollection IRegKeyCollection.FindSubKeysByValue(object value, bool recurse, StringMatch mode)
            => FindSubKeysByValue(value, recurse, mode);

        IRegKeyCollection IRegKeyCollection.FindSubKeysByValue<T>(T value, bool recurse, StringMatch mode)
            => FindSubKeysByValue(value, recurse, mode);
    }
}
