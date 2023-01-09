using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegExample.Data;

namespace UserRegExample.GenericRepo
{
    public class Repository<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _Context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext Context)
        {
            _Context = Context;
            entities = _Context.Set<T>();
        }

        public void Create(T model)
        {
            entities.Add(model);
            _Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int Id)
        {
            return entities.Find(Id);
        }
    }
}
