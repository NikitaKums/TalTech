using System.Collections.Generic;
using ee.itcollege.nikita.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser :  IdentityUser<int>, IDomainEntity // PK type is int
    {
        public ICollection<Order> Orders { get; set; }
    }

}