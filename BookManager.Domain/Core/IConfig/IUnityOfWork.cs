using BookManager.Domain.Core.IRepository;

namespace BookManager.Domain.Core.IConfig
{
    public interface IUnityOfWork
    {
        IAuthorRepo Authors { get; }
        IBookRepo Books { get; }
        ICategoryRepo Categories { get; }
        IUserRepo Users { get; }

        Task CompleteAsync();
    }
}
