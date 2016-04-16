namespace QuiGon.Infrastructure.Entities
{
    /// <summary>
    /// Субъектный социальной сети
    /// </summary>
    public class SocialSubject
    {
        public SocialSubject(long id, string name, EntityType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Имя/названия
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType Type { get; }
    }
}