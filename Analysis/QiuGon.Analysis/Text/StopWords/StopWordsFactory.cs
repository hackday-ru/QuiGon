using System.Collections.Generic;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;

namespace QuiGon.Analysis.Text.StopWords
{
    /// <summary>
    /// Фабрика стоп-слов
    /// </summary>
    /// <remarks>
    /// Не содержит символы и числа
    /// </remarks>
    public class StopWordsFactory
    {
        #region Singletone

        private static StopWordsFactory _instance;

        public static StopWordsFactory Instance
        {
            get { return _instance ?? (_instance = new StopWordsFactory()); }
        }

        #endregion

        #region Путь к словарем стоп-слов

        private const string RussianStopWordsPath = @"Text\StopWords\Storage\Russian.sw";

        #endregion

        private readonly Dictionary<Language, HashSet<string>> _stopWords; 

        private StopWordsFactory()
        {
            _stopWords = new Dictionary<Language, HashSet<string>>();
            Init();
        }

        private void Init()
        {
            _stopWords.Add(Language.Russian, GetRussianStopWords());
        }

        /// <summary>
        /// Возвращает множество стоп-слов для русского языка
        /// </summary>
        /// <returns></returns>
        private HashSet<string> GetRussianStopWords()
        {
            //TODO здесь нужен стемминг
            return WordsFromFileProvider.GetStopWordsFromFile(RussianStopWordsPath) ??  new HashSet<string>();
        }

        /// <summary>
        /// Возвращает множество стоп-слов
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public HashSet<string> GetStopWords(Language language)
        {
            return _stopWords.ContainsKey(language)
                ? _stopWords[language]
                : null;
        }
    }
}