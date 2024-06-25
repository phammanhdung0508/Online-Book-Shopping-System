using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IJWTTokenProvider
{
    string CreateToken(User user);
}