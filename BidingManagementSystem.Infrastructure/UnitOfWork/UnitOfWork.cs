using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Infrastructure.Data;
using BidingManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		public ITenderRepository tenderRepository { get; private set; }
		public IBidRepository bidRepository { get; private set; }
		public IGenericRepository<User> userRepository { get; private set; }

		public IEvaluationRepository evaluationRepository { get; private set; }
		public IGenericRepository<Award> awardRepository { get; private set; }
		public IBidDocumentRepository bidDocumentRepository { get; private set; }
		public ITenderDocumentRepository tenderDocumentRepository { get; private set; }

		public UnitOfWork(ApplicationDbContext dbcontext)
		{
			_context = dbcontext;
			tenderRepository = new TenderRepository(_context);
			bidRepository = new BidRepository(_context);
			userRepository = new GenericRepository<User>(_context);
			evaluationRepository = new EvaluationRepository(_context);
			awardRepository = new GenericRepository<Award>(_context);
			bidDocumentRepository = new BidDocumentRepository(_context);
			tenderDocumentRepository = new TenderDocumentRepository(_context);
		}
		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
