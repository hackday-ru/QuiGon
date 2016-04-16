using QuiGon.Analysis.Text.Lemmatisation.Solarix;

namespace QuiGon.Analysis.Text.Lemmatisation
{
    public class Lemmatyzer
    {
        private readonly ILemmatyzerFacade _lemmatyzer;

        public Lemmatyzer()
        {
            _lemmatyzer = new SolarixLemmatyzer();
        }

        public TextAnalysisRequest GetLemma(TextAnalysisRequest request)
        {
            return _lemmatyzer.MakeLemmas(request);
        }

        public string GetLemma(string word)
        {
            return _lemmatyzer.MakeLemmas(word);
        }
    }
}