using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LigaSportaDemo.Models;
using Microsoft.AspNetCore.Authorization;
using LigaSportaDemo.Models.ViewModels;

namespace LigaSportaDemo.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AccountController(UserManager<IdentityUser> user, SignInManager<IdentityUser> sign)
		{
			this.userManager = user;
			this.signInManager = sign;
		}

		public IActionResult Login(string returnUrl)
		{
			return View(new LoginModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel login)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = await userManager.FindByNameAsync(login.Name);
				if (user!=null)					
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user,login.Password,false,false)).Succeeded)
					{
						return Redirect(login?.ReturnUrl ?? Statics.rootUrl);
					}
				}
			}

			ModelState.AddModelError("", "Invalid login or password");
			return View(login);
		}

		public IActionResult Logout(string returnUrl)
		{
			signInManager.SignOutAsync();
			return Redirect(returnUrl ?? Statics.rootUrl);
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
