using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib
{
    internal class RegValueCollection : ICollection<RegValue>
    {
        private RegValue[] _values = Array.Empty<RegValue>();
        private int _count = 0;
        private int _capacity => _values?.Length ?? 0;

        public int Count => throw new NotImplementedException();
        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(RegValue item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(RegValue item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(RegValue[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<RegValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(RegValue item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
