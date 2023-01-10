using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Data;
using UserRegExample.Models;
using UserRegExample.ViewModels;

namespace UserRegExample.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public RoomController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            ViewBag.RoomType = _Context.RoomTypes.ToList();
            RoomViewModel vm = new RoomViewModel();
             vm.RoomFacilities = _Context.Facilitys.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var room = new Room
            {

            };

            _Context.Rooms.Add(room);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected string UploadFile(RoomViewModel vm)
        {
            string fileName = null;
            if(vm.RoomPictureUri != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.RoomPictureUri.FileName;
                string filepath = Path.Combine(uploadDir, fileName);
                using var fileStream = new FileStream(filepath, FileMode.Create);
                vm.RoomPictureUri.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
