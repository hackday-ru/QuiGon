using System.Collections.Generic;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Analysis.Text.Stemming;

namespace QuiGon.Analysis.Sentiment
{
    public class SentimentStatisticFactory
    {
        private const string RussianSentimentDictionaryPath = @"Sentiment\Storage\Russian.sd";

        #region Seignleton

        private static SentimentStatisticFactory _instance;

        public static SentimentStatisticFactory Instance
        {
            get { return _instance ?? (_instance = new SentimentStatisticFactory()); }
        }

        #endregion

        private readonly Dictionary<Language, SentimentDictionary> _sentimentDictionaries;

        private SentimentStatisticFactory()
        {
            _sentimentDictionaries = new Dictionary<Language, SentimentDictionary>();
            _sentimentDictionaries.Add(Language.Russian, GetRussianDictionary());
        }

        private SentimentDictionary GetRussianDictionary()
        {
            var stemmer = new Stemmer();

            var dictionary = new Dictionary<string, double>();
            var pairs = WordsFromFileProvider.GetStopWordsFromFile(RussianSentimentDictionaryPath);
            foreach (var pair in pairs)
            {
                var splittedPair = pair.Split(TextOperationConstants.DelimiterChar);
                if (splittedPair.Length < 2) continue;
                double value;
                double.TryParse(splittedPair[1], out value);
                dictionary[stemmer.Stem(splittedPair[0])] = value;
            }
            return new SentimentDictionary(dictionary);
        }

        public SentimentDictionary GetDictionary(Language language)
        {
            if (!_sentimentDictionaries.ContainsKey(language)) return null;

            return _sentimentDictionaries[language];
        }
    }
}