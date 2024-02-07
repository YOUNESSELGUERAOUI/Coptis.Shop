using Microsoft.AspNetCore.Identity;

namespace Coptis.Shop.Infrastructure.Entities;

public class TUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public virtual ICollection<TSale> TSales { get; set; }
}
