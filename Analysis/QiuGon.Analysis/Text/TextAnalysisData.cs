using System.Collections.Generic;

namespace QuiGon.Analysis.Text
{
    public class TextAnalysisData : IAnalysisData
    {
        public TextAnalysisData(IReadOnlyList<string> data)
        {
            Data = data;
        }

        public IReadOnlyList<string> Data { get; } 
    }
}