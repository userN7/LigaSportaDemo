using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LigaSportaDemo.Models;
using LigaSportaDemo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static LigaSportaDemo.Models.Statics;
using Microsoft.AspNetCore.Authorization;

namespace LigaSportaDemo.Controllers
{
	public class FilmsController : Controller
	{
		private readonly DataContext dataContext;
		private readonly IWebHostEnvironment appEnvironment;
		
		public FilmsController(DataContext dataCxt, IWebHostEnvironment appEnvironment)
		{
			this.dataContext = dataCxt;
			this.appEnvironment = appEnvironment;
		}

		[Authorize]
		[HttpGet]
		public IActionResult DeleteFilmView(long filmId)
		{
			var filmToDel = dataContext.Films.Find(filmId);
			return	View("DeleteFilm", filmToDel);
		}

		[Authorize]		
		public IActionResult DeleteFilm(long filmId)
		{
			var filmToDel = dataContext.Films.Find(filmId);
			if (filmToDel!=null
				&&filmToDel.CreatorUser == HttpContext.User.Identity.Name)
			{
				
			try
			{
			 dataContext.Films.Remove(filmToDel);
			 dataContext.SaveChanges();
			}
			catch (Exception)
			{

				throw;
			}
			}
			return RedirectToAction("Index");
		}

		public IActionResult ShowFilm(long filmId)
		{
			var film = dataContext.Films.Find(filmId);
			if (film != null)
				return View("ShowFilm", film);
			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult UpdateFilm(long filmId)
		{
			var film = dataContext.Films.Find(filmId);
			if (film!=null
				&&film.CreatorUser==HttpContext.User.Identity.Name)
			{
				ViewData["isUpdate"] = 1;
				return View("CreateUpdateFilm", film);
			}

			 return RedirectToAction("Index"); ;
		}

		[Authorize]
		public IActionResult CreateFilm()
		{
			return View("CreateUpdateFilm", new FilmInfo() { ReleaseDate = DateTime.Now });
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateUpdateFilm(IFormFile uploadedFile, FilmInfo film, int isUpdate)
		{			
			string path = String.Empty;
			film.CreatorUser = HttpContext.User.Identity.Name;
			if (uploadedFile != null)
			{

				var imgDir = appEnvironment.WebRootPath + postersPath;
				if (!System.IO.Directory.Exists(imgDir))
				{
					System.IO.Directory.CreateDirectory(imgDir);

				}
				

				path = postersPath + uploadedFile.FileName;

				try
				{
					// сохраняем файл в папку Files в каталоге wwwroot
					using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.OpenOrCreate))
					{
						await uploadedFile.CopyToAsync(fileStream);
					}
				}
				catch (Exception)
				{

					throw;
				}
				
			}

			if (path!=String.Empty)
			{
				film.PosterPath = path;
			}
			try
			{
				if (isUpdate == 1)
				{
					dataContext.Films.Update(film);

				}
				if (isUpdate == 0)//Создание
				{
					film.CreatorUser = HttpContext.User.Identity.Name;
					dataContext.Films.Add(film);
				}
				dataContext.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
			


			return RedirectToAction("Index");
		}
		public IActionResult Index(int filmPage =1)
		{
			var pageInfo = new PagingInfo()
			{
				CurrentPage = filmPage,				
				TotalItems = dataContext.Films.Count()
			};

			return View(new FilmsListViewModel()
			{
				Films = dataContext.Films
					.OrderBy(f => f.Name)
					.Skip((pageInfo.CurrentPage - 1) * pageInfo.ItemsPerPage)
					.Take(pageInfo.ItemsPerPage),
				PagingInfo = pageInfo
			}); ;
		}
	}
}
