using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Data;
using UserRegExample.Models;

namespace UserRegExample.Repo
{
    public class EmployeeRepo : IEmployee
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Where(x => x.Id == id).SingleOrDefault();
        }

        public void CreateEmp(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
        }

        public void UpdateEmp(Employee emp)
        {
            _context.Employees.Update(emp);
            _context.SaveChanges();
        }

        public void DeleteEmp(Employee emp)
        {
            _context.Employees.Remove(emp);
            _context.SaveChanges();
        }
    }
}
