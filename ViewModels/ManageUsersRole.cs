using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Models;

namespace UserRegExample.ViewModels
{
    public class ManageUsersRole
    {
        public ApplicationUser AppUser { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> roles { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AppClaims { get; set; }
    }
}
