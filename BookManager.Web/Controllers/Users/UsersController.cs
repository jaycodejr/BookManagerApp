using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Helper;
using BookManager.Domain.Models;
using BookManager.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Web.Controllers.Users
{
    public class UsersController : Controller
    {
        private readonly IUnityOfWork _uow;

        public UsersController(IUnityOfWork uow)
        {
            _uow = uow;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _uow.Users.FindAll(u => !u.IsDeleted);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var checkUser = (ICollection<User>)await _uow.Users.FindAll(u => u.UserName.ToLower() == user.UserName.ToLower());

                if (checkUser.Count > 0)
                {
                    ModelState.AddModelError("", "Username already exist");
                    return View(user);
                }

                var password = Common.HashPassword(string.Format("{0}@{1}", user.UserName, DateTime.Now.Year.ToString()));
                user.Password = password;
                var isCreated = await _uow.Users.Add(user);
                if (isCreated)
                {
                    await _uow.CompleteAsync();
                }
                TempData[isCreated ? "success" : "error"] = isCreated ? "User created successfully" : "Failed to create user";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var user = await _uow.Users.Find((int)id);
            if (user == null)
            {
                return NotFound();
            }

            var editUser = new EditUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Status = user.Status,
            };
            return View(editUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {

            var user = await _uow.Users.Find(model.Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Failed to update user");
                return View(model);
            }

            var checkUser = (ICollection<User>)await _uow.Users.FindAll(u => u.UserName.ToLower() == model.UserName.ToLower() && u.Id != model.Id);
            if (checkUser.Count > 0)
            {
                ModelState.AddModelError("", "Username already exist");
                return View(model);
            }

            user.Name = model.Name; 
            user.UserName = model.UserName;
            user.Role = model.Role;
            user.Status = model.Status;
            
            var isUpdated = await _uow.Users.Update(user);

            if (isUpdated)
            {
                await _uow.CompleteAsync();
                TempData["success"] = "User updated successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to update user");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var user = await _uow.Users.Find((int)id);
            if (user == null)
            {
                return NotFound();
            }

            var deleteUser = new EditUserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Status = user.Status,
            };
            return View(deleteUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditUserViewModel model)
        {

            var user = await _uow.Users.Find(model.Id);
            if (user == null)
            {
                ModelState.AddModelError("", "Failed to delete user");
                return View(model);
            }

            user.Status = false;
            user.IsDeleted = true;

            var isDeleted = await _uow.Users.Update(user);

            if (isDeleted)
            {
                await _uow.CompleteAsync();
                TempData["success"] = "User deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Failed to delete user");
            return View(model);
        }
    }
}
