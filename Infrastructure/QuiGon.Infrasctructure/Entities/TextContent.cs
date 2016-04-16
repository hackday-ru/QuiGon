namespace QuiGon.Infrastructure.Entities
{
    /// <summary>
    /// Текстовое содержимое
    /// </summary>
    public class TextContent : ISubjectActionContent
    {
        /// <summary>
        /// Текстовое содержимое
        /// </summary>
        public TextContent(string content)
        {
            Content = content;
        }

        /// <summary>
        /// Содержимое
        /// </summary>
        public string Content { get; }
    }
}