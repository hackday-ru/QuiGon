using System;
using System.Collections.Generic;
using QuiGon.Analysis.Filters;
using QuiGon.Analysis.Filters.TextFilters;
using QuiGon.Analysis.Helpers;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Analysis.Text.Lemmatisation;
using QuiGon.Analysis.Text.Lemmatisation.Solarix;
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

                //TestFiltration();

                TestStemming();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
                SolarixLemmatyzer.Instance.Dispose();
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
                Console.Write("{0} ", data);

            }
        }

        #endregion


        #region Стемминг

        private static void TestStemming()
        {
            var filters = new List<IFilter>
            {
                new TextToLowerCaseFilter(),
                new NumericFilter(),
                new PunctuationFilter(),
                new StemingFilter(),
                new StopWordsFilter()
                
            };
            var filterChain = new FilterChain(filters);

            var testText =
                @"Два года назад в BP, одной из крупнейших нефтяных компаний мира, 
                опасались, что международные санкции сильно повредят ее бизнесу в России. 
                Оказалось, что ничего подобного — Россия приносит BP все больше денег, 
                особенно если считать не в абсолютных цифрах, а в доле в прибыли. 
                Мировые цены на нефть находятся на очень низком — в долларах — уровне. Что это значит
                для всех нефтяных компаний в мире? Падение доходов.Что это значит для России?
                Девальвацию и, следовательно, экономию на рабочей силе и на налогах — опять же, 
                если перевести расходы в доллары. В итоге в 2015 году BP заработал 22 % всей своей доналоговой 
                прибыли в России.Эти деньги западная компания получила, будучи одним из крупнейших 
                акционеров «Роснефти» (с 2013 года ей принадлежит 19,75 % акций госконцерна). 
                Ни в 2013 - м, ни в 2014 - м российская доля в выручке не превышала 20 %. 
                Конечно, падение цен на нефть влияет и на «Роснефть». В 2015 году BP, согласно 
                подсчетам Bloomberg, получила в России 80 миллиардов рублей (на восемь миллиардов больше, 
                чем год назад). В долларах доход BP упал на 30 %, но в среднем по всему миру этот показатель 
                у компании составил 51 %. 
                Вложения в «Роснефть» помогли BP решить и другую проблему: истощения месторождений. 
                Россия — единственное место в мире, где BP смогла заместить добычу получением лицензий 
                на новые месторождения.Показатель замещения у BP в 2015 году составил 61 %.Без России этот 
                показатель упал бы до 34 %. Мы ели суп, а вдоль аллеи стояли раскидистые ели";

            var words = WordsSeparator.SeparateWords(testText);
            var filteredText = filterChain.Filter(new TextAnalysisRequest(-1, SubjectActionType.Post, new TextAnalysisData(words)));

            Console.WriteLine(testText);
            foreach (var data in (filteredText.Data as TextAnalysisData).Data)
            {
                Console.Write("{0} ", data);

            }
        }

        #endregion

    }
}
