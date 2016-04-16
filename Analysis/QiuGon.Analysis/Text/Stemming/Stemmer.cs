using QuiGon.Analysis.Text.Stemming.LingvoNet;

namespace QuiGon.Analysis.Text.Stemming
{
    public class Stemmer
    {
        private readonly IStemerFacade _stemmer;

        public Stemmer()
        {
            _stemmer = new LingovNetStemmer();
        }

        public TextAnalysisRequest Stem(TextAnalysisRequest request)
        {
            return _stemmer.Stem(request);
        }
        
        public string Stem(string word)
        {
            return _stemmer.Stem(word);
        }
    }
}