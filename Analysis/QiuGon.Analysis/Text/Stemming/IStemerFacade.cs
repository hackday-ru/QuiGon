namespace QuiGon.Analysis.Text.Stemming
{
    public interface IStemerFacade
    {
        TextAnalysisRequest Stem(TextAnalysisRequest request);

        string Stem(string text);
    }
}