using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Models;
using BookManager.Web.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Web.Controllers.Books
{
    public class BooksController : Controller
    {
        private readonly IUnityOfWork _table;
        public BooksController(IUnityOfWork table)
        {
            _table = table;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _table.Books.FindAll();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            var createVM = new CreateBookViewModel()
            {
                AuthorList = await PopulateAuthorList(),
                CategoryList = await PopulateCategoryList()
            };

            return View(createVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (ModelState.IsValid)
            {

                var author = await _table.Authors.Find(model.AuthorId);
                var category = await _table.Categories.Find(model.CategoryId);

                if (author == null)
                {
                    ModelState.AddModelError("Failed", "Author not found");
                    return View(model);
                }

                if (category == null)
                {
                    ModelState.AddModelError("Failed", "Category not found");
                    return View(model);
                }

                var newBook = new Book();
                newBook.Author = author;
                newBook.Category = category;
                newBook.Title = model.Title;
                newBook.Description = model.Description;
                newBook.ToTalPage = model.TotalPage;
                newBook.PublishedDate = model.PublishedDate;

                var isCreated = await _table.Books.Add(newBook);

                if (isCreated)
                {
                    await _table.CompleteAsync();
                    TempData["success"] = "Book created successfully";
                }
                else
                {
                    TempData["error"] = "Failed to create book";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var book = await _table.Books.Find((int)id);

            if (book == null)
            {
                return NotFound();
            }

            var editBook = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                TotalPage = book.ToTalPage,
                PublishedDate = book.PublishedDate,
                CategoryId = book.Category.Id,
                CategoryList = await PopulateCategoryList(),
                AuthorId = book.Author.Id,
                AuthorList = await PopulateAuthorList(),
            };

            return View(editBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(EditBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = await _table.Books.Find(model.Id);

                if (book == null)
                {
                    return BadRequest();
                }

                var author = await _table.Authors.Find(model.AuthorId);
                var category = await _table.Categories.Find(model.CategoryId);

                if (author == null)
                {
                    ModelState.AddModelError("Failed", "Invalid author");
                    return View(model);
                }

                if (category == null)
                {
                    ModelState.AddModelError("Failed", "Invalid category");
                    return View(model);
                }

                book.Title = model.Title;
                book.Description = model.Description;
                book.Author = author;
                book.Category = category;
                book.ToTalPage = model.TotalPage;
                book.PublishedDate = model.PublishedDate;               

                var isUpdated = await _table.Books.Update(book);

                if (isUpdated)
                {
                    await _table.CompleteAsync();
                }

                TempData[isUpdated ? "success" : "error"] = isUpdated ? "Book updated successfully" : "Failed to update book";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var book = await _table.Books.Find((int)id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }

            var book = await _table.Books.Find((int)id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            var findBook = await _table.Books.Find(book.Id);
            if (findBook != null) 
            {
                var isDeleted = await _table.Books.Remove(findBook);
                if (isDeleted)
                {
                    await _table.CompleteAsync();
                }
                TempData[isDeleted ? "success" : "error"] = isDeleted ? "Book deleted successfully" : "Failed to delete book";
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        private async Task<IEnumerable<Author>> PopulateAuthorList()
        {
            return await _table.Authors.FindAll();
        }

        private async Task<IEnumerable<Category>> PopulateCategoryList()
        {
            return await _table.Categories.FindAll();
        }
    }
}
