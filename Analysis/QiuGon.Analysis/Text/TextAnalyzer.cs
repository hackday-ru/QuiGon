using System;
using QuiGon.Analysis.Filters;

namespace QuiGon.Analysis.Text
{
    public class TextAnalyzer
    {
        private readonly FilterChain _filterChain;

        public TextAnalyzer(FilterChain filterChain)
        {
            if (filterChain == null) throw new ArgumentNullException(nameof(filterChain));

            _filterChain = filterChain;
        }

    }
}