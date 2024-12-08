using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechNation.Data.Context;

namespace TechNation.Data.Repositorio
{
    public class BaseRepositorio<T> where T : class
    {
        protected readonly LogAppContext _context;

        private DbSet<T> _dataSet;

        public BaseRepositorio(LogAppContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                _dataSet.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return null;
            }
            return item;
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataSet.ToListAsync();
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                return null;
            }
        }

    }
}
