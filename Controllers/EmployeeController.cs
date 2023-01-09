using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Models;
using UserRegExample.Repo;

namespace UserRegExample.Controllers
{
    public class EmployeeController : Controller
    {
        protected IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        public IActionResult Index()
        {
            var emps = _employee.GetAll();
            return View(emps);
        }

        public IActionResult Details(int id)
        {
            Employee emp = _employee.GetById(id);
            return View(emp);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            _employee.CreateEmp(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee emp = _employee.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _employee.UpdateEmp(emp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee emp = _employee.GetById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            _employee.DeleteEmp(emp);
            return RedirectToAction("Index");
        }
    }
}
