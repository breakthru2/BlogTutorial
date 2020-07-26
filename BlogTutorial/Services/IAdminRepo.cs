using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial.Services
{
    public interface IAdminRepo
    {
        Task AddUser(IdentityUser user, string role, string password);

        Task<IdentityUser> GetUser(string Id);

        Task<List<IdentityUser>> GetAllUsers();

        Task<bool> SaveChangesAsync();
    }
}
