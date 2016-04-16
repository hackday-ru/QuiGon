using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Analysis.Text.Stemming;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public class StemingFilter : ITextFilter
    {
        private readonly Stemmer _stemmer;

        public StemingFilter()
        {
            _stemmer = new Stemmer();
        }

        public IAnalysisRequest Filter(IAnalysisRequest request)
        {
            return Filter(request as TextAnalysisRequest, Language.Russian);
        }

        public TextAnalysisRequest Filter(TextAnalysisRequest request, Language language)
        {
            if (language == Language.Russian)
            {
                return _stemmer.Stem(request);
            }
            return request;
        }
    }
}