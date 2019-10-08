using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Context
{
    public class IdentityUserDbContext : IdentityDbContext<User>
    {
        public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options)
            : base(options)
        {

        }
    }

}
