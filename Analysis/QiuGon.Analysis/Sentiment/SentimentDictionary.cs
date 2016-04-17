using System;
using System.Collections.Generic;

namespace QuiGon.Analysis.Sentiment
{
    public class SentimentDictionary
    {
        private readonly Dictionary<string, double> _dictionary;

        public SentimentDictionary(Dictionary<string, double> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            _dictionary = dictionary;
        }

        public double this[string term]
        {
            get
            {
                double value;
                _dictionary.TryGetValue(term, out value);
                return value;
            }
        }
    }
}