using System;
using System.Collections.Generic;
using QuiGon.Analysis.Filters;
using QuiGon.Analysis.Filters.TextFilters;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TestLanguageDetection();

                TestFiltration();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        #region Определение языка

        private static void TestLanguageDetection()
        {
            var languageDetector = new LanguageDetector();
            var action = new SubjectAction(0, SubjectActionType.Post, new TextContent("Привет. Это русский язык"), -1, -1, null);
            var language = languageDetector.Detect(action);
            if (language == null)
            {
                Console.WriteLine("Fail to detect");
                return;
            }

            WriteLanguage(language.Value);
        }

        private static void WriteLanguage(Language language)
        {
            switch (language)
            {
                case Language.Russian:
                    Console.WriteLine("Русский");
                    break;
                default:
                    Console.WriteLine("Неизвестно");
                    break;
            }
        }

        #endregion

        #region Фильтрация

        private static void TestFiltration()
        {
            var filters = new List<IFilter>
            {
                new TextToLowerCaseFilter(),
                new NumericFilter(),
                new PunctuationFilter(),
                new StopWordsFilter()
            };
            var filterChain = new FilterChain(filters);

            var testText =
                "Привет! Я хочу протестировать этот текст. Тут будут просто частые слова. Типа: я, он что? " +
                "Ох и ах! А также мы добавим числа. 23. И 234так. И так123. И т1234567890ак. А много лишних символов .,:!" +
                "$%^&*())_{}:\'\"";

            var words = WordsSeparator.SeparateWords(testText);
            var filteredText = filterChain.Filter(new TextAnalysisRequest(-1, SubjectActionType.Post, new TextAnalysisData(words)));

            Console.WriteLine(testText);
            foreach (var data in (filteredText.Data as TextAnalysisData).Data)
            {
                Console.Write("{)} ", data);

            }
        }

        #endregion

    }
}
