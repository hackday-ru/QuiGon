namespace QuiGon.Analysis.Text.Lemmatisation
{
    public interface ILemmatyzerFacade
    {
        TextAnalysisRequest MakeLemmas(TextAnalysisRequest request);

        string MakeLemmas (string text);
    }
}
