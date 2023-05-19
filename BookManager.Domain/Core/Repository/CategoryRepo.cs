using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Data;
using BookManager.Domain.Models;
using Microsoft.Extensions.Logging;

namespace BookManager.Domain.Core.Repository
{
    public class CategoryRepo : BaseRepo<Category, int>, ICategoryRepo
    {
        public CategoryRepo(BookDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
