using BookManager.Domain.Core.IConfig;
using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Core.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Data
{
    public class UnityOfWork : IUnityOfWork, IDisposable
    {
        private readonly BookDbContext _context;
        private readonly ILogger _logger;

        public IAuthorRepo Authors { get; private set; }
        public IBookRepo Books { get; private set; }
        public ICategoryRepo Categories { get; private set; }
        public IUserRepo Users { get; private set; }

        public UnityOfWork(BookDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<UnityOfWork>();

            Authors = new AuthorRepo(_context, _logger);
            Books = new BookRepo(_context, _logger);
            Categories = new CategoryRepo(_context, _logger);
            Users = new UserRepo(_context, _logger);

            Categories = new CategoryRepo(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
