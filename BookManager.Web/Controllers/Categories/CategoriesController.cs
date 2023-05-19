using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Web.Controllers.Categories
{
    public class CategoriesController : Controller
    {
        private readonly IUnityOfWork _table;

        public CategoriesController(IUnityOfWork table) => _table = table;


        public async Task<IActionResult> Index()
        {
            var categories = await _table.Categories.FindAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {

                var newCategory = await _table.Categories.Add(category);
                await _table.CompleteAsync();

                if (!newCategory)
                {
                    TempData["error"] = "Failed to create category";
                }

                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id ==null || id==0)
            {
                return BadRequest();
            }

            var category = await _table.Categories.Find((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {

            if (ModelState.IsValid)
            {
              
                var isUpdated = await _table.Categories.Update(category);

                if (!isUpdated)
                {
                    ModelState.AddModelError("Failed", "Failed to update category");
                    return View(category);
                }
                await _table.CompleteAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var category = await _table.Categories.Find((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            var findCategory = await _table.Categories.Find(category.Id);
            if (findCategory != null)
            {
                if (findCategory.Books?.Count > 0)
                {
                    TempData["error"] = "Delete failed. Book(s) have been assigned to the Category";
                }
                else
                {
                    var isDeleted = await _table.Categories.Remove(findCategory);
                    if (isDeleted)
                    {
                        await _table.CompleteAsync();
                    }
                    TempData[isDeleted ? "success" : "error"] = isDeleted ? "Category deleted successfully" : "Failed to delete category";
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
