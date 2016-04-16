using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Filters
{
    public interface IFilter
    {
        IAnalysisRequest Filter(IAnalysisRequest request);
    }
}