using System.Collections.Generic;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public class TextToLowerCaseFilter : ITextFilter
    {
        public IAnalysisRequest Filter(IAnalysisRequest request)
        {
            return Filter(request as TextAnalysisRequest, Language.Russian);
        }

        public TextAnalysisRequest Filter(TextAnalysisRequest request, Language language)
        {
            var textData = request?.Data as TextAnalysisData;
            if (textData == null) return null;

            var filteredData = new List<string>();
            foreach (var data in textData.Data)
            {
                filteredData.Add(data.ToLower());
            }

            return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredData));
        }
    }
}