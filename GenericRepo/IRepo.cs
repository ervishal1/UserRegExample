using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegExample.GenericRepo
{
    public interface IRepo<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);

        void Create(T model);
    }
}
