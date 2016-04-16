using System;
using System.Collections.Generic;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Filters
{
    public class FilterChain
    {
        private readonly List<IFilter> _filters;

        public FilterChain(List<IFilter> filters)
        {
            if (filters == null || filters.Count == 0) throw new ArgumentNullException(nameof(filters));

            _filters = new List<IFilter>(filters);
        }

        public IAnalysisRequest Filter(IAnalysisRequest content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            var filteredContent = content;
            foreach (var filter in _filters)
            {
                filteredContent = filter.Filter(filteredContent);
            }

            return filteredContent;
        }
    }
}