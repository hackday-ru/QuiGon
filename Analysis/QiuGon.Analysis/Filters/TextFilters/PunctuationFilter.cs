using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public class PunctuationFilter : ITextFilter
    {
        private const string PunctuationRegexPattern = @"[^\w\s\d]+";

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
                var filteredValue = Regex.Replace(data, PunctuationRegexPattern, TextOperationConstants.ReplacedString);
                if(String.IsNullOrWhiteSpace(filteredValue)) continue;

                filteredData.Add(filteredValue);
            }

            return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredData));
        }
    }
}