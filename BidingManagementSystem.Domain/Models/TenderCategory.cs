using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class TenderCategory
	{
		[ForeignKey("TenderId")]
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		[ForeignKey("CategoryId")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
