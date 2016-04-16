using System;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TestLanguageDetection();
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
            var action = new SubjectAction(0, SubjectACtionType.Post, new TextContent("Привет. Это русский язык"), -1, -1, null);
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
    }
}
