namespace BookManager.Domain.Models
{
    public class Base<T>
    {
        public virtual T? Id { get; set; }
    }
}
