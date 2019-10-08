using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Services.Context;
using Blog.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.DbService
{
    public interface IIdentityService
    {
        Task<IList<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        string GetUserRoleById(string userId);
        Task<bool> BlockUserByIdAsync(string userId);
        Task<bool> UnBlockUserByIdAsync(string userId);
    }
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IdentityUserDbContext _context;
        private readonly IdentityUserDbContextInMemory _context;

        public IdentityService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, /*IdentityUserDbContext context,*/ IdentityUserDbContextInMemory context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> BlockUserByIdAsync(string userId)
        {
            User user = await _userManager
                .Users.FirstOrDefaultAsync(x => x.Id == userId);
            await _userManager.AddToRoleAsync(user, "Block");
            await _userManager.RemoveFromRoleAsync(user, "User");
            return true;
        }

        public async Task<bool> UnBlockUserByIdAsync(string userId)
        {
            User user = await _userManager
                .Users.FirstOrDefaultAsync(x => x.Id == userId);
            await _userManager.AddToRoleAsync(user, "User");
            await _userManager.RemoveFromRoleAsync(user, "Block");
            return true;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            User user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            return user;
        }

        public string GetUserRoleById(string userId)
        {
            var roleId = _context.UserRoles.FirstOrDefault(u => u.UserId == userId);
            var roleName = _context.Roles.FirstOrDefault(r => r.Id == roleId.RoleId);
            return roleName.Name.ToString();
        }
    }
}
