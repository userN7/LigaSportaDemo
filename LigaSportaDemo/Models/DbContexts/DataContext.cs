using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LigaSportaDemo.Models
{
	public class DataContext : DbContext	
	{
		public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }
		public DbSet<FilmInfo> Films { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FilmInfo>()
				.HasIndex(f => f.Name);
		}
	}
}
