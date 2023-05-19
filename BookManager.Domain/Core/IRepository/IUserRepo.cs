using BookManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Core.IRepository
{
    
    public interface IUserRepo:IBaseRepo<User,int>
    {
    }

    
}
