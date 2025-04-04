namespace RepositoryStore.Models;

public class EntityBase
{
    public int  Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}