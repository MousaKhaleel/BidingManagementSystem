using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Repositories
{
	public class BidRepository : IBidRepository
	{
		private readonly ApplicationDbContext _context;

		public BidRepository(ApplicationDbContext context)
		{
			_context = context;
		}
	}
}
