using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.GenericRepo;
using UserRegExample.Models;

namespace UserRegExample.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IRepo<Product> _Repo;
        public IActionResult Index()
        {
            var Product = _Repo.GetAll();
            return View(Product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            _Repo.Create(model);
            return RedirectToAction("Index");
        }
    }
}
