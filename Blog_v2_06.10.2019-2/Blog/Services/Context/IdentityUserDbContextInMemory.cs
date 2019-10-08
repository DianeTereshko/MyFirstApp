using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Context
{
    public class IdentityUserDbContextInMemory : IdentityDbContext<User>
    {
        public IdentityUserDbContextInMemory()
        {
        }

        public IdentityUserDbContextInMemory(DbContextOptions<IdentityUserDbContextInMemory> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryUserDataBase");
        }

    }
}
