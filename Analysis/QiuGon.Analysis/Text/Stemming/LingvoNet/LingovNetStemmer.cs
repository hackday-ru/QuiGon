using System.Collections.Generic;

namespace QuiGon.Analysis.Text.Stemming.LingvoNet
{

    public class LingovNetStemmer : IStemerFacade
    {
        public TextAnalysisRequest Stem(TextAnalysisRequest request)
        {
            var textData = request?.Data as TextAnalysisData;
            if (textData == null) return null;
            
            var filteredWords = new List<string>();
            foreach (var word in textData.Data)
            {
                var stemmedWord = LingvoNET.Stemmer.Stemm(word);

                filteredWords.Add(stemmedWord);
            }

            return new TextAnalysisRequest(request.ActionId, request.Type, new TextAnalysisData(filteredWords));
        }

        public string Stem(string word)
        {
            return LingvoNET.Stemmer.Stemm(word);
        }
    }
}