using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Text
{
    public class TextAnalysisRequest : IAnalysisRequest
    {
        public TextAnalysisRequest(long actionId, SubjectActionType type, IAnalysisData data)
        {
            ActionId = actionId;
            Type = type;
            Data = data;
        }

        public long ActionId { get; }

        public SubjectActionType Type { get; }

        public IAnalysisData Data { get; }
    }
}