using System;
using QuiGon.Analysis.LanguageDetection.NTextCat;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.LanguageDetection
{
    public class LanguageDetector
    {
        private readonly ITextLanguageDetectorFacade _textLanguageDetector;

        public LanguageDetector()
        {
            _textLanguageDetector = new NTextCatLanguageDetector();
        }

        public Language? Detect(SubjectAction action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            switch (action.Type)
            {
                case SubjectACtionType.Comment:
                case SubjectACtionType.Repost:
                case SubjectACtionType.Post:
                    return Detect(action.Content);
                default:
                    return null;
            }
        }

        private Language? Detect(ISubjectActionContent content)
        {
            var textContent = content as TextContent;
            if (String.IsNullOrEmpty(textContent?.Content)) return null;
            return _textLanguageDetector.Detect(textContent.Content);
        }
    }
}
