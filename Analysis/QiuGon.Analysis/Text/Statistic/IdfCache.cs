using System;
using System.Collections.Generic;

namespace QuiGon.Analysis.Text.Statistic
{
    public class IdfCache
    {

        #region Singletone

        private static IdfCache _instance;

        public static IdfCache Instance
        {
            get { return _instance ?? (_instance = new IdfCache()); }
        }

        #endregion

        private readonly Dictionary<string, HashSet<long>> _cache;
        private readonly HashSet<long> _documents; 

        private IdfCache()
        {
            _cache = new Dictionary<string, HashSet<long>>();
            _documents = new HashSet<long>();
        }

        public int GetDocumentsWithTermCount(string term)
        {
            if (String.IsNullOrEmpty(term)) return 1;

            if (!_cache.ContainsKey(term)) return 1;

            var documents = _cache[term];
            return documents.Count;
        }

        public void Push(string term, long documentId)
        {
            _documents.Add(documentId);
            if (!_cache.ContainsKey(term))
            {
                _cache[term] = new HashSet<long> {documentId};
                return;
            }
            _cache[term].Add(documentId);
        }

        public int GetDocumentsCount()
        {
            return _documents.Count;
        }
    }
}