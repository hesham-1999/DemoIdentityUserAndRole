using Bl.Interfaces;
using DAL.AppContext;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DemoContext context;

        public GenericRepository(DemoContext _context)
        {
            this.context = _context;
        }
        public  async Task Create(T entity)
        {
           await context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<T> Get(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
           
            return await context.Set<T>().ToListAsync();
        }

        public async Task<int> Save()
        {
         return await context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
