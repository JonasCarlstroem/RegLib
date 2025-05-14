using RegLib.Elements;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RegLib.Collections.Base
{
    public abstract class FilteredRegBaseCollection<T, TCollection> : IReadOnlyList<T>
        where TCollection : RegBaseCollection<T>
        where T : IRegElement
    {
        private protected readonly TCollection _source;
        private protected readonly List<int> _filteredIndices;

        public FilteredRegBaseCollection(TCollection source, Func<T, bool> predicate)
        {
            _source = source;
            _filteredIndices = new List<int>();

            for(int i = 0; i < _source.Count; i++)
            {
                var item = _source[i];
                if (predicate(item))
                    _filteredIndices.Add(i);
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _filteredIndices.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _source[_filteredIndices[index]];
            }
        }

        public int Count => _filteredIndices.Count;

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
