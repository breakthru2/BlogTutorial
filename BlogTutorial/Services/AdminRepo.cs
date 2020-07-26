using BlogTutorial.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial.Services
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminRepo(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task AddUser(IdentityUser user, string role, string password)
        {
            //var roleInDb = await _roleManager.FindByNameAsync(role);
            if (!(await _roleManager.RoleExistsAsync(role)))
            {
                // create a new role
                var newRole = new IdentityRole(role);
               await _roleManager.CreateAsync(newRole);
            }
            var newUser = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email
            };
            // add a new user
            await _userManager.CreateAsync(newUser, password);

            // assign the new user to created or exisiting role
            await _userManager.AddToRoleAsync(newUser, role);


        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync());
        }

        public async Task<IdentityUser> GetUser(string Id)
        {
            var user = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
            return (user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
