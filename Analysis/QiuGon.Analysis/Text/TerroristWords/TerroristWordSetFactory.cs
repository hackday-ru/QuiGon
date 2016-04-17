using System.Collections.Generic;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text.Stemming;

namespace QuiGon.Analysis.Text.TerroristWords
{
    public class TerroristWordSetFactory
    {
        #region Singleton

        private static TerroristWordSetFactory _instance;

        public static  TerroristWordSetFactory Instance
        { get { return _instance ?? (_instance = new TerroristWordSetFactory()); } }

        #endregion

        private const string RussianTerroristWordsSetPath = @"Text\TerroristWords\Storage\Russian.tws";


        private readonly Dictionary<Language, HashSet<string>> _terrorisWordsSet;

        private TerroristWordSetFactory()
        {
            _terrorisWordsSet = new Dictionary<Language, HashSet<string>>();
            Init();
        }

        private void Init()
        {
            _terrorisWordsSet.Add(Language.Russian, GetRussianStopWords());
        }

        /// <summary>
        /// Возвращает множество стоп-слов для русского языка
        /// </summary>
        /// <returns></returns>
        private HashSet<string> GetRussianStopWords()
        {
            var stemmer = new Stemmer();

            var stemmedWords = new HashSet<string>();
            var stopWords = WordsFromFileProvider.GetStopWordsFromFile(RussianTerroristWordsSetPath) ?? new HashSet<string>();
            foreach (var stopWord in stopWords)
            {
                stemmedWords.Add(stemmer.Stem(stopWord));
                //stemmedWords.Add(stopWord);
            }

            return stemmedWords;
        }

        /// <summary>
        /// Возвращает множество стоп-слов
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public HashSet<string> GetStopWords(Language language)
        {
            return _terrorisWordsSet.ContainsKey(language)
                ? _terrorisWordsSet[language]
                : null;
        }
    }
}