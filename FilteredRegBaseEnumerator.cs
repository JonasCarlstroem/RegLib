using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegLib
{
    public class FilteredRegBaseEnumerator<T> : IEnumerator<T>
    {
        private readonly RegBaseCollection<T> _source;
        private readonly List<int> _filteredIndices;
        private int _position;

        public FilteredRegBaseEnumerator(RegBaseCollection<T> source, List<int> filteredIndices)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _filteredIndices = filteredIndices ?? throw new ArgumentNullException(nameof(filteredIndices));
            _position = -1;
        }

        public T Current => _source[_filteredIndices[_position]];

        object IEnumerator.Current => Current;

        public bool MoveNext() => ++_position < _filteredIndices.Count;
        public void Reset() => _position = -1;
        public void Dispose() { }
    }
}
