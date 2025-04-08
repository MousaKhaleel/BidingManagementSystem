using BidingManagementSystem.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class GetBidDto
	{
		public decimal Amount { get; set; }
		public BidStatus Status { get; set; }
		public int TenderId { get; set; }
		public string BidderId { get; set; }
	}
}
