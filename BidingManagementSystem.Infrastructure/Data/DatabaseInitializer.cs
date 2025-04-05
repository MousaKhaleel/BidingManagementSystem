using BidingManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidingManagementSystem.Infrastructure.Data
{
	public class DatabaseInitializer
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<DatabaseInitializer> _logger;

		public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		public async Task InitializeAsync()
		{
			using var scope = _serviceProvider.CreateScope();
			var services = scope.ServiceProvider;

			try
			{
				var dbContext = services.GetRequiredService<ApplicationDbContext>();
				if (dbContext.Database.CanConnect())
				{
					await dbContext.Database.MigrateAsync();

					await SeedRolesAsync(services);
					await SeedAdminUserAsync(services);
				}
				else
				{
					_logger.LogWarning("Database is not accessible.");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while initializing the database.");
			}
		}

		private async Task SeedRolesAsync(IServiceProvider services)
		{
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
			var roles = new[] { "Admin", "ProcurementOfficer", "Bidder", "Evaluator" };//TODO

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

		private async Task SeedAdminUserAsync(IServiceProvider services)
		{
			var userManager = services.GetRequiredService<UserManager<User>>();
			var email = "Admin@adm.com";
			var password = "admin1234";

			if (await userManager.FindByEmailAsync(email) == null)
			{
				var adminUser = new User
				{
					UserName = "Admin",
					Email = email,
				};

				var result = await userManager.CreateAsync(adminUser, password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
			}
			//TODO: seed industry, type, and location.
		}
	}
}