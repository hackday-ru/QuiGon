using QuiGon.Analysis.LanguageDetection;
using QuiGon.Analysis.Text;
using QuiGon.Infrastructure.Entities;

namespace QuiGon.Analysis.Filters.TextFilters
{
    public interface ITextFilter : IFilter
    {
        /// <summary>
        /// Фильтрует текстовое соде
        /// </summary>
        /// <param name="request"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        TextAnalysisRequest Filter(TextAnalysisRequest request, Language language);
    }
}