using BidingManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Interfaces
{
	public interface ITenderRepository : IGenericRepository<Tender>
	{
		Task<IEnumerable<Tender>> GetOpenTendersAsync();
		Task<Tender> GetTenderByIdAsync(int id);
		Task<IEnumerable<Tender>> GetTendersByCategoryAsync(int categoryId);
	}
}
