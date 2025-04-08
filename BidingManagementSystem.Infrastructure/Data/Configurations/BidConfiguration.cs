using BidingManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Data.Configurations
{
	public class BidConfiguration : IEntityTypeConfiguration<Bid>
	{
		public void Configure(EntityTypeBuilder<Bid> builder)
		{
			builder.Property(e => e.CreateDate)
				.HasDefaultValueSql("GETDATE()");

			builder.HasOne(b => b.Bidder)
				.WithMany(u => u.Bids)
				.HasForeignKey(b => b.BidderId)
				.OnDelete(DeleteBehavior.Restrict); ;

			builder.HasOne(b => b.Tender)
				.WithMany(t => t.Bids)
				.HasForeignKey(b => b.TenderId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
