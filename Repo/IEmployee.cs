using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Models;

namespace UserRegExample.Repo
{
    public interface IEmployee
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetById(int id);
        public void CreateEmp(Employee emp);
        public void UpdateEmp(Employee emp);
        public void DeleteEmp(Employee emp);
    }
}
