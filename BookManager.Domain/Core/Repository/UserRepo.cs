using BookManager.Domain.Core.IRepository;
using BookManager.Domain.Data;
using BookManager.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Core.Repository
{
    public class UserRepo : BaseRepo<User, int>, IUserRepo
    {
        public UserRepo(BookDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
