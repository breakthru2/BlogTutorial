using BlogTutorial.Data;
using BlogTutorial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminRepo(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void AddCategory(Category category)
        {
             _context.Categories.Add(category);
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public async Task<IdentityResult> AddUser(IdentityUser user, string role, string password)
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
            var result = await _userManager.CreateAsync(newUser, password);

           
            // assign the new user to created or exisiting role
            await _userManager.AddToRoleAsync(newUser, role);

            return result;


        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return (await _context.Users.ToListAsync());
        }

        public async Task<Post> GetPost(Guid Id)
        {
            return await _context.Posts.Where(p => p.Id == Id).FirstOrDefaultAsync();
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

        public async Task<bool> UserLogin(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded) {
                return true;
                    }
            return false;
        }
    }
}
