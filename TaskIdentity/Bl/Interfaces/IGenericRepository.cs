using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Interfaces
{
    public interface IGenericRepository<T>  where T : class
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Save();
    }
}
