using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Data;
using BookManager.Domain.Models;
using Microsoft.Extensions.Logging;

namespace BookManager.Domain.Core.Repository
{
    public class AuthorRepo : BaseRepo<Author, int>, IAuthorRepo
    {
        public AuthorRepo(BookDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
