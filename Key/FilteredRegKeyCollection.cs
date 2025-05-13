using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib.Key
{
    public class FilteredRegKeyCollection : FilteredRegBaseCollectionTemp<RegKey>
    {
        public FilteredRegKeyCollection(RegKeyCollection source, Func<RegKey, bool> predicate)
            : base(source, predicate) { }

        public override IEnumerator<RegKey> GetEnumerator()
        {
            return new FilteredRegKeyEnumerator(_source, _filteredIndices);
        } 
    }
    //public class FilteredRegKeyCollection : IReadOnlyList<RegKey>
    //{
    //    private readonly RegKeyCollection _source;
    //    private readonly List<int> _filteredIndices;

    //    public FilteredRegKeyCollection(RegKeyCollection source, Func<RegKey, bool> predicate)
    //    {
    //        _source = source ?? throw new ArgumentNullException(nameof(source));
    //        _filteredIndices = new List<int>();

    //        for (int i = 0; i < _source.Count; i++)
    //        {
    //            var item = _source[i];
    //            if (predicate(item))
    //                _filteredIndices.Add(i);
    //        }
    //    }

    //    public RegKey this[int index]
    //    {
    //        get
    //        {
    //            if (index < 0 || index >= _filteredIndices.Count)
    //                throw new ArgumentOutOfRangeException(nameof(index));

    //            return _source[_filteredIndices[index]];
    //        }
    //    }

    //    public int Count => _filteredIndices.Count;

    //    public IEnumerator<RegKey> GetEnumerator()
    //    {
    //        return new FilteredRegKeyEnumerator(_source, _filteredIndices);
    //    }

    //    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    //    private struct FilteredRegKeyEnumerator : IEnumerator<RegKey>
    //    {
    //        private readonly RegKeyCollection _source;
    //        private readonly List<int> _filteredIndices;
    //        private int _position;

    //        public FilteredRegKeyEnumerator(RegKeyCollection source, List<int> filteredIndices)
    //        {
    //            _source = source ?? throw new ArgumentNullException(nameof(source));
    //            _filteredIndices = filteredIndices ?? throw new ArgumentNullException(nameof(filteredIndices));
    //            _position = -1;
    //        }

    //        public RegKey Current => _source[_filteredIndices[_position]];

    //        object IEnumerator.Current => Current;

    //        public bool MoveNext()
    //        {
    //            _position++;
    //            return _position < _filteredIndices.Count;
    //        }

    //        public void Reset()
    //        {
    //            _position = -1;
    //        }

    //        public void Dispose()
    //        { }
    //    }
    //}
}
