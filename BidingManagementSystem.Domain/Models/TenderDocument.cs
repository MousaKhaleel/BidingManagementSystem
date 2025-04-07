using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Domain.Models
{
	public class TenderDocument
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TenderDocumentId { get; set; }
		public string DocumentName { get; set; }
		[Required]
		public string DocumentPath { get; set; }
		public DateTime CreateDate { get; set; }

		[ForeignKey("TenderId")]
		public int TenderId { get; set; }
		public Tender Tender { get; set; }
	}
}
