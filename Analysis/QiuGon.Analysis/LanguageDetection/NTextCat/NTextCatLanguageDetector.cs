using System;
using System.Linq;
using NTextCat;

namespace QuiGon.Analysis.LanguageDetection.NTextCat
{
    /// <summary>
    /// Фасад библиотеки NTextCat для определения языка
    /// </summary>
    public class NTextCatLanguageDetector : ITextLanguageDetectorFacade
    {
        #region Названия языков на английском

        private const string RussianLanguageName = "rus";

        #endregion

        private const string DefaultLanguageProfilePath = @"LanguageDetection\NTextCat\LanguageModels\Core14.profile.xml";

        private readonly RankedLanguageIdentifier _identifier;
        
        public NTextCatLanguageDetector(string profilePath = null)
        {
            var factory = new RankedLanguageIdentifierFactory();
            _identifier = factory.Load(profilePath ?? DefaultLanguageProfilePath);
        }

        public Language Detect(string text)
        {
            var languages = _identifier.Identify(text);
            var mostCertainLanguage = languages.FirstOrDefault();

            if (mostCertainLanguage == null) return Language.Unknown;

            return GetLanguage(mostCertainLanguage.Item1.Iso639_3);
        }

        /// <summary>
        /// Возвращает язык по его имени
        /// </summary>
        /// <param name="language">Названия языке на английском</param>
        /// <returns></returns>
        private Language GetLanguage(string language)
        {
            if (String.Equals(language, RussianLanguageName, StringComparison.InvariantCultureIgnoreCase))
            {
                return Language.Russian;
            }

            return Language.Unknown;
        }
    }
}