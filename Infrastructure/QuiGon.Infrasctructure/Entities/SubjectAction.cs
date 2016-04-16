namespace QuiGon.Infrastructure.Entities
{
    /// <summary>
    /// Действие субъекта
    /// </summary>
    public class SubjectAction
    {
        public SubjectAction(long id, SubjectACtionType type, ISubjectActionContent content, long authorId, long recipientId, SubjectAction parentSubjectAction)
        {
            Id = id;
            Type = type;
            Content = content;
            AuthorId = authorId;
            RecipientId = recipientId;
            ParentSubjectAction = parentSubjectAction;
        }

        /// <summary>
        /// Идентификатор действия
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Тип действия
        /// </summary>
        public SubjectACtionType Type { get; }

        /// <summary>
        /// Содержимое действия
        /// </summary>
        public ISubjectActionContent Content { get; }

        /// <summary>
        /// Автор действия
        /// </summary>
        public long AuthorId { get; }

        /// <summary>
        /// Кому действие адресовано
        /// </summary>
        public long RecipientId { get; }

        /// <summary>
        /// Действия-родитель
        /// </summary>
        public SubjectAction ParentSubjectAction { get; }
    }
}