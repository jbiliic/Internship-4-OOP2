namespace OOP2.Domain.Common.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
