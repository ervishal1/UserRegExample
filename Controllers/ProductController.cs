using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Data;
using UserRegExample.GenericRepo;
using UserRegExample.Helper;
using UserRegExample.Models;

namespace UserRegExample.Controllers
{
    public class ProductController : Controller
    {
        protected readonly IRepo<Product> _Repo;
        protected readonly ApplicationDbContext _Context;

        public ProductController(IRepo<Product> repo, ApplicationDbContext context)
        {
            _Repo = repo;
            _Context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 3;
            var product = _Context.Products.AsNoTracking();
            return View(await PaginatedList<Product>.CreateAsync(product,pageNumber,pageSize));
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

        public IActionResult Search(string searchProductName = null)
        {
            if(String.IsNullOrEmpty(searchProductName))
            { 
                return View();
            }
            var searchResult = _Context.Products.Where(x => x.Title.Contains(searchProductName)).ToList();
            if(searchResult.Count == 0)
            {
                ViewBag.products = "Search Product Not Found";
                return View();
            }

            return View(searchResult);
        }
    }
}
