using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Helper;
using BookManager.Domain.Models;
using BookManager.Web.Models;
using BookManager.Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnityOfWork _uow;
        private readonly ITokenServiceRepo _tokenServiceRepo;

        public HomeController(IUnityOfWork uow,ITokenServiceRepo tokenServiceRepo)
        {
            _uow = uow;
            _tokenServiceRepo = tokenServiceRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var getUser =(ICollection<User>) await _uow.Users.FindAll(u => u.UserName.ToLower() == model.UserName.ToLower());
            if (getUser.Count == 0)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            var user = getUser.First();

            var validatePass = Common.ValidatePassword(model.Password, user.Password);

            if (!validatePass)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            var token = _tokenServiceRepo.GetToken(user);


            return RedirectToAction("Index", "Books");
        }

    }
}