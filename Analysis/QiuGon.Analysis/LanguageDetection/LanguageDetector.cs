using System;
using System.Linq;
using QuiGon.Analysis.LanguageDetection.NTextCat;
using QuiGon.Analysis.Text;
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
                case SubjectActionType.Comment:
                case SubjectActionType.Repost:
                case SubjectActionType.Post:
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


        public Language? Detect(TextAnalysisRequest data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            switch (data.Type)
            {
                case SubjectActionType.Comment:
                case SubjectActionType.Repost:
                case SubjectActionType.Post:
                    var content = data.Data as TextAnalysisData;
                    if (content?.Data == null) return null;
                    return Detect(content.Data.FirstOrDefault());
                default:
                    return null;
            }
        }

        private Language? Detect(string text)
        {
            if (String.IsNullOrEmpty(text)) return null;
            return _textLanguageDetector.Detect(text);
        }
    }
}
