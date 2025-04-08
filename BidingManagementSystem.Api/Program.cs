using BidingManagementSystem.Application.Commands.Bid.DeleteBidAsync;
using BidingManagementSystem.Application.Commands.Bid.DeleteBidDocumentAsync;
using BidingManagementSystem.Application.Commands.Bid.SubmitBid;
using BidingManagementSystem.Application.Commands.Bid.UpdateBidAsync;
using BidingManagementSystem.Application.Commands.Bid.UploadBidDocumentsAsync;
using BidingManagementSystem.Application.Commands.Category.AddCategoryAsync;
using BidingManagementSystem.Application.Commands.Evaluation.AwardBid;
using BidingManagementSystem.Application.Commands.Evaluation.EvaluateBid;
using BidingManagementSystem.Application.Commands.Tender.CreateTender;
using BidingManagementSystem.Application.Commands.Tender.DeleteTender;
using BidingManagementSystem.Application.Commands.Tender.DeleteTenderDocumentAsync;
using BidingManagementSystem.Application.Commands.Tender.UpdateTender;
using BidingManagementSystem.Application.Commands.Tender.UploadTenderDocumentAsync;
using BidingManagementSystem.Application.Interfaces;
using BidingManagementSystem.Application.Queries.Bid.GetBidByIdAsync;
using BidingManagementSystem.Application.Queries.Bid.GetBidDocumentsAsync;
using BidingManagementSystem.Application.Queries.Bid.GetBidsByTenderIdAsync;
using BidingManagementSystem.Application.Queries.Category.GetAllCategoriesAsync;
using BidingManagementSystem.Application.Queries.Evaluation.GetAwardedBidAsync;
using BidingManagementSystem.Application.Queries.Evaluation.GetBidScoreAsync;
using BidingManagementSystem.Application.Queries.Tender.GetAllTendersAsync;
using BidingManagementSystem.Application.Queries.Tender.GetOpenTendersAsync;
using BidingManagementSystem.Application.Queries.Tender.GetTenderByIdAsync;
using BidingManagementSystem.Application.Queries.Tender.GetTenderDocumentsAsync;
using BidingManagementSystem.Application.Queries.Tender.GetTendersByCategoryAsync;
using BidingManagementSystem.Application.Services;
using BidingManagementSystem.Domain.Interfaces;
using BidingManagementSystem.Domain.Models;
using BidingManagementSystem.Infrastructure.Data;
using BidingManagementSystem.Infrastructure.Repositories;
using BidingManagementSystem.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(
	options =>
	{
		//TODO:temp for dev, remove
		options.Password.RequiredUniqueChars = 0;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = false;
		options.Password.RequiredLength = 8;
		options.Password.RequireNonAlphanumeric = false;
	})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//builder.Services.AddAuthentication(options =>
//{
//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//	{
//		options.TokenValidationParameters = new TokenValidationParameters()
//		{
//			ValidateActor = true,
//			ValidateIssuer = true,
//			ValidateAudience = true,
//			RequireExpirationTime = true,
//			ValidateIssuerSigningKey = true,
//			ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
//			ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
//		};
//	});//TODO fix

builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(DeleteBidCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(DeleteBidDocumentCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(SubmitBidCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(UpdateBidCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(UploadBidDocumentsCommandHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(AddCategoryCommandHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(AwardBidCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(EvaluateBidCommandHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(CreateTenderCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(DeleteTenderCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(DeleteTenderDocumentCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(UpdateTenderCommandHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(UploadTenderDocumentCommandHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(GetBidByIdQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetBidDocumentsQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetBidsByTenderIdQueryHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(GetAllCategoriesQueryHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(GetAwardedBidQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetBidScoreQueryHandler).Assembly);

	configuration.RegisterServicesFromAssembly(typeof(GetAllTendersQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetOpenTendersQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetTenderByIdQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetTenderDocumentsQueryHandler).Assembly);
	configuration.RegisterServicesFromAssembly(typeof(GetTendersByCategoryQueryHandler).Assembly);

});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//TODO
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITenderRepository, TenderRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<DatabaseInitializer>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
	await initializer.InitializeAsync();
}

app.Run();
