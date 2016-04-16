using System.Collections.Generic;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Analysis.Text.StopWords;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public class StopWordsFilter : ITextFilter
    {
        public IAnalysisRequest Filter(IAnalysisRequest request)
        {
            return Filter(request as TextAnalysisRequest, Language.Russian);
        }

        public TextAnalysisRequest Filter(TextAnalysisRequest request, Language language)
        {
            var textData = request?.Data as TextAnalysisData;
            if (textData == null) return null;

            var stopWords = StopWordsFactory.Instance.GetStopWords(language);

            var filteredWords = new List<string>();
            foreach (var word in textData.Data)
            {
                if (stopWords.Contains(word)) continue;

                filteredWords.Add(word);
            }

            return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredWords));
        }
    }
}