using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using CoinAPI.Models;

namespace CoinAPI.Models
{
	public class CoinAPIDBContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public CoinAPIDBContext(DbContextOptions<CoinAPIDBContext> options, IConfiguration configuration) : base(options)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			var connectionString = Configuration.GetConnectionString("CoinDataService");
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}

		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Email> Emails { get; set; } = null!;
		public DbSet<Coin> Coins { get; set; } = null!;
	}
}
