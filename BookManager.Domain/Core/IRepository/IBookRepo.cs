using BookManager.Domain.Models;

namespace BookManager.Domain.Core.IRepository
{
    public interface IBookRepo:IBaseRepo<Book,int>
    {
    }
}
