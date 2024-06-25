using Domain.Primitives;

namespace Domain.Entities;

public sealed class User : Entity
{
    public string Email { get; private set; } = string.Empty;
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public string Phone { get; private set; } = string.Empty;
    public bool IsRemoved { get; private set; } = false;
    public DateTime RemovedAt { get; private set; } = DateTime.MinValue;

    /*One -------------------------------------------------*/
    public Guid RoleId { get; private set; }
    public Role? Role { get; private set; }
    /*Many -------------------------------------------------*/
    public ICollection<Order>? Orders { get; private set; }

    public User() { }

    public class Builder
    {
        private User _user = new User();

        public Builder SetID(Guid id) { _user.Id = id; return this; }
        public Builder SetEmail(string email) { _user.Email = email; return this; }
        public Builder SetPasswordHash(byte[] passwordHash) {  _user.PasswordHash = passwordHash; return this; }
        public Builder SetPasswordSalt(byte[] passwordSalt) {  _user.PasswordSalt = passwordSalt; return this; }
        public Builder SetPhone(string phone) { _user.Phone = phone; return this; }
        public Builder SetIsRemoved(bool isRemoved) { _user.IsRemoved = isRemoved; return this; }
        public Builder SetRemovedAt(DateTime removeAt) { _user.RemovedAt = removeAt; return this; }

        public User Build() => _user;
    }
}
