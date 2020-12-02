using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LigaSportaDemo.Models.Statics;

namespace LigaSportaDemo.Models.DbContexts
{
    public class FilmsSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var maxFilms = 10_000;
            var dataContext = app.ApplicationServices
                                .CreateScope()
                                .ServiceProvider
                                .GetRequiredService<DataContext>();
            if (dataContext.Database.GetPendingMigrations().Any())
            {
                dataContext.Database.Migrate();
            }

            var absentFilmsCount =  maxFilms - dataContext.Films.Count();

            if (absentFilmsCount>0)
			{
                var rand = new Random();
                var currNum = dataContext.Films.Count();
                var userCount = users.Count;
				while (absentFilmsCount>0)
				{
                    dataContext.Films.Add(new FilmInfo
                    {
                        CreatorUser = users[rand.Next(0, userCount)],
                        Description = "Описание "+ currNum,
                        Director = "Директор " + currNum,
                        Name = "Фильм " + currNum,
                        ReleaseDate = new DateTime(rand.Next(1980,2020),rand.Next(1,12),rand.Next(1,28))                        
                    }) ;
                    currNum++;
                    absentFilmsCount--;

                }
                dataContext.SaveChanges();
            }
        }
    }
}
