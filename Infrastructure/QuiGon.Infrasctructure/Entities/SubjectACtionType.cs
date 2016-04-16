namespace QuiGon.Infrastructure.Entities
{
    /// <summary>
    /// Тип действия субъекта
    /// </summary>
    public enum SubjectACtionType : byte
    {
        /// <summary>
        /// Неизвестный тип
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Публикация
        /// </summary>
        Post = 1,

        /// <summary>
        /// Отметка "Мне нравится"
        /// </summary>
        Like = 2, 

        /// <summary>
        /// Комментарий
        /// </summary>
        Comment = 3,

        /// <summary>
        /// Репост
        /// </summary>
        Repost = 4
    }
}