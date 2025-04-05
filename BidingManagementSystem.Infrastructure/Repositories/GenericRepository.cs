using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext dbcontext)
		{
			_context = dbcontext;
			_dbSet = dbcontext.Set<T>();
		}
		//TODO
		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task<T> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
