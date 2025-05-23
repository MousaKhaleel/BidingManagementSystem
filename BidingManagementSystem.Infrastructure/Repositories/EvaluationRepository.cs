﻿using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Repositories
{
	public class EvaluationRepository: GenericRepository<Evaluation>, IEvaluationRepository
	{
		private readonly ApplicationDbContext _context;

		public EvaluationRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Evaluation> GetEvaluationByBidIdAsync(int bidId)
		{
			return await _context.Evaluations.Where(x=>x.BidId== bidId).FirstOrDefaultAsync();
		}
	}
}
