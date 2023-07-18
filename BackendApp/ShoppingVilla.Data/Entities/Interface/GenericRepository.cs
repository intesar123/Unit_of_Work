using Microsoft.EntityFrameworkCore;
using ShoppingVilla.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingVilla.Data.Entities.Interface
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context=context;
        }

        public virtual void  CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async virtual Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async virtual Task<T> Login(T entity)
        {
            return  await _context.Set<T>().FindAsync("");
        }

        public Task<int> Logout(string token)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
