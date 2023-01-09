using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Data;
using UserRegExample.Models;
using UserRegExample.ViewModels;

namespace UserRegExample.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        protected readonly ApplicationDbContext _context;

        public TestController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ManageUsersRole UserRoles = new ManageUsersRole();

            var user = _context.Users.Where(x => x.Id == id).SingleOrDefault();
            var userInRole = _context.UserRoles.Where(x => x.UserId == id).Select(x => x.RoleId).ToList();

            UserRoles.roles = await _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userInRole.Contains(x.Id)
            }).ToListAsync();

            UserRoles.AppUser = user;

            return View(UserRoles);
        }

        [HttpPost]
        public IActionResult Edit(ManageUsersRole model)
        {
            var selectedRoleId = model.roles.Where(x => x.Selected).Select(x => x.Value);
            var alradyExistRoleId = _context.UserRoles.Where(x => x.UserId == model.AppUser.Id).Select(x => x.RoleId ).ToList();
            var toAdd = selectedRoleId.Except(alradyExistRoleId);
            var toRemove = alradyExistRoleId.Except(selectedRoleId);

            foreach (var item in toRemove)
            {
                _context.UserRoles.Remove(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }

            foreach (var item in toAdd)
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
