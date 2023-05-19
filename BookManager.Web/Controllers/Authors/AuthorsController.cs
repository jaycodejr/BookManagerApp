using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Web.Controllers.Authors
{
    public class AuthorsController : Controller
    {
        private readonly IUnityOfWork _table;
        public AuthorsController(IUnityOfWork table)
        {
            _table = table;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await _table.Authors.FindAll();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {

                var newAuthor = await _table.Authors.Add(author);
                await _table.CompleteAsync();

                if (!newAuthor)
                {
                    TempData["error"] = "Failed to create author";
                }

                TempData["success"] = "Author created successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var author = await _table.Authors.Find((int)id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = await _table.Authors.Update(author);

                if (isUpdated)
                {
                    await _table.CompleteAsync();
                    TempData["success"] = "Author updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Failed", "Failed to update author");
                return View(author);
            }
            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var author = await _table.Authors.Find((int)id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var author = await _table.Authors.Find((int)id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Author author)
        {
            var findAuthor = await _table.Authors.Find(author.Id);
            if (findAuthor != null)
            {
                if (findAuthor.Books?.Count > 0)
                {
                    TempData["error"] = "Delete failed. Book(s) have been assigned to the Author";
                }
                else
                {
                    var isDeleted = await _table.Authors.Remove(findAuthor);
                    if (isDeleted)
                    {
                        await _table.CompleteAsync();
                    }
                    TempData[isDeleted ? "success" : "error"] = isDeleted ? "Author deleted successfully" : "Failed to delete author";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
