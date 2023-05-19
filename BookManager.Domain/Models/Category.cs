using System.ComponentModel.DataAnnotations;

namespace BookManager.Domain.Models
{
    public class Category:Base<int>
    {     
        public string Name { get; set; } = string.Empty;

        public bool Status { get; set; }

        public virtual ICollection<Book>? Books { get; set; }
        public virtual User CreatedBy { get; set; } = new();        
    }
}
