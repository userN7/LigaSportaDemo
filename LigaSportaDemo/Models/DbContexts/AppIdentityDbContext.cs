using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LigaSportaDemo.Models
{
	public class AppIdentityDbContext : IdentityDbContext<IdentityUser>	
	{
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> opts) : base(opts) { }
	}
}
