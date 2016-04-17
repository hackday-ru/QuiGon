using System;
using System.Collections.Generic;

namespace QuiGon.Analysis.Text.Statistic
{
    public class TfidfCalculator
    {
        private readonly Dictionary<string, int> _counter;
        private int _documentsCount;

        public TfidfCalculator(string[] terms, long documentId)
        {
            _counter = new Dictionary<string, int>();

            foreach (var term in terms)
            {
                if (!_counter.ContainsKey(term))
                {
                    _counter[term] = 1;
                }
                else
                {
                    _counter[term]++;
                }
                _documentsCount++;
                IdfCache.Instance.Push(term, documentId);
            }
        }

        public double GetMetric(string term)
        {
            var numberOfOccurrences = _counter.ContainsKey(term) ? _counter[term] : 1;
            var tf = (double)numberOfOccurrences/_documentsCount;

            var idf = Math.Abs(Math.Log((IdfCache.Instance.GetDocumentsCount() + 0.5)/(IdfCache.Instance.GetDocumentsWithTermCount(term) + 0.7)));

            return tf*idf;
        }
    }
}