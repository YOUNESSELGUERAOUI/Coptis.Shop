using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Core.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserAsync(string userId);
}
