using Domain.Primitives;

namespace Domain.Entities;

public sealed class Role : Entity
{
    public string Name { get; private set; }
    public Role(Guid id, string name) : base(id) { Name = name; }

    /*Many -------------------------------------------------*/
    public ICollection<User>? Users { get; private set; }
}
