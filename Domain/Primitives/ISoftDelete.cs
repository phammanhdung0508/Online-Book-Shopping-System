namespace Domain.Primitives;

public interface ISoftDelete
{
    bool IsDeleted { get; }
}
