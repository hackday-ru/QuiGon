using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Analysis.Text.Lemmatisation;
using QuiGon.Analysis.Text.Lemmatisation.Solarix;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public class LemmaFilter : ITextFilter
    {
        private readonly ILemmatyzerFacade _lemmatyzer;

        public LemmaFilter()
        {
            _lemmatyzer = new SolarixLemmatyzer();
        }

        public IAnalysisRequest Filter(IAnalysisRequest request)
        {
            return Filter(request as TextAnalysisRequest, Language.Russian);
        }

        public TextAnalysisRequest Filter(TextAnalysisRequest request, Language language)
        {
            if (language == Language.Russian)
            {
                return _lemmatyzer.MakeLemmas(request);
            }
            return request;
        }
    }
}