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
            var userInClaims = _context.UserClaims.Where(x => x.UserId == id).Select(x => x.ClaimValue).ToList();


            UserRoles.AppUser = user;
            UserRoles.roles = await _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userInRole.Contains(x.Id)
            }).ToListAsync();

            UserRoles.AppClaims = ClaimStore.All.Select(x => new SelectListItem()
            {
                Text = x.Type,
                Value = x.Value,
                Selected = userInClaims.Contains(x.Value)

            }).ToList();

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

            var selectedClaimId = model.AppClaims.Where(x => x.Selected).Select(x => x.Value);
            var alradyExistClaims = _context.UserClaims.Where(x => x.UserId == model.AppUser.Id).Select(x => x.Id.ToString()).ToList();
            var toAddClaim = selectedClaimId.Except(alradyExistClaims);
            var toRemoveClaims = alradyExistClaims.Except(selectedClaimId);

            foreach (var item in toRemoveClaims)
            {
                _context.UserClaims.Remove(new IdentityUserClaim<string>
                {
                    Id = Convert.ToInt32(item),
                    UserId = model.AppUser.Id
                });
            }

            foreach (var item in toAddClaim)
            {
                _context.UserClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = model.AppUser.Id,
                    ClaimValue = item,
                    ClaimType = item
                });
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
