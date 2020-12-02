using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LigaSportaDemo.Models.Statics;

namespace LigaSportaDemo.Models.DbContexts
{
	public class IdentitySeedData
	{
       
            
            private const string password = "Secret123$";

            public static async void EnsurePopulated(IApplicationBuilder app)
            {

                AppIdentityDbContext context = app.ApplicationServices
                    .CreateScope().ServiceProvider
                    .GetRequiredService<AppIdentityDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                UserManager<IdentityUser> userManager = app.ApplicationServices
                                .CreateScope().ServiceProvider
                                .GetRequiredService<UserManager<IdentityUser>>();

                foreach (string userName in users)
			    {
                 IdentityUser user = await userManager.FindByNameAsync(userName);
                 if (user == null)
                 {
                   user = new IdentityUser(userName);                    
                   await userManager.CreateAsync(user, password);
                 }
                }         


            }
        
    }
}
