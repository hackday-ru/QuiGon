using System.Security.Cryptography.X509Certificates;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis
{
    public interface IAnalysisRequest
    {
        long ActionId { get; } 

        SubjectActionType Type { get; }

        IAnalysisData Data { get; }
    }
}