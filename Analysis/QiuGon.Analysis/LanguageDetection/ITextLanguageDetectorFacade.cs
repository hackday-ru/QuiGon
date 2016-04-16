namespace QuiGon.Analysis.LanguageDetection
{
    /// <summary>
    /// Фасад для библиотек, определяющий язык текста
    /// </summary>
    public interface ITextLanguageDetectorFacade
    {
        /// <summary>
        /// Определяет язык представленного текста
        /// </summary>
        /// <param name="text">Тест для определения</param>
        /// <returns>Язык</returns>
        Language Detect(string text);
    }
}