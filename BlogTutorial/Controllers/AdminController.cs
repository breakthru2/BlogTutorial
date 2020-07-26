using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogTutorial.Services;
using BlogTutorial.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogTutorial.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IAdminRepo _repo;

        public AdminController(IAdminRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = vm.Email
                };
                _repo.UserLogin(user, vm.Password);
                return RedirectToAction(nameof(Dashboard));
            }
            return View(vm);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = vm.Email
                };

                await _repo.AddUser(user, vm.Role, vm.Password);
                await _repo.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            return View(vm);
        }

        public async Task<IActionResult> ListUsers()
        {
            return View( await _repo.GetAllUsers());
        }
    }
}
