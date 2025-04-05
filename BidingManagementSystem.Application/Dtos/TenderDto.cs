using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Application.Dtos
{
	public class TenderDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime ClosingDate { get; set; }
		public decimal? Budget { get; set; }
	}
}
