using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.GenericRepo;
using UserRegExample.Models;

namespace UserRegExample.Controllers
{
    public class RoomTypeController : Controller
    {
        protected readonly IRepo<RoomType> _Repo;

        public RoomTypeController(IRepo<RoomType> repo)
        {
            _Repo = repo;
        }

        public IActionResult Index()
        {
            var types = _Repo.GetAll();
            return View(types);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoomType model)
        {
            _Repo.Create(model);
            return RedirectToAction("Index");
        }
    }
}
