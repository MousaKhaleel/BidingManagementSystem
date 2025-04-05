using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BidingManagementSystem.Domain.Models.Enums;

namespace BidingManagementSystem.Domain.Models
{
	public class Evaluation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EvaluationId { get; set; }
		public decimal Score { get; set; }
		public DateTime EvaluationDate { get; set; }
		public EvaluationCriteria Criteria { get; set; }

		public int TenderId { get; set; }
		public Tender Tender { get; set; }
		public string EvaluatorId { get; set; }
		public User Evaluator { get; set; }
	}
}
