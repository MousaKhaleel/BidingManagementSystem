using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class TenderCategory
	{
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
