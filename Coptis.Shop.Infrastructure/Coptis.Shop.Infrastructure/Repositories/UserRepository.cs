using Coptis.Shop.Core.Interfaces;
using Coptis.Shop.Core.Models;

namespace Coptis.Shop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
