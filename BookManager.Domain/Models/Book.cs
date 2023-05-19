using System.ComponentModel.DataAnnotations;

namespace BookManager.Domain.Models
{
    public class Book : Base<int>
    {
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int ToTalPage { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public virtual User CreatedBy { get; set; } = new();
        public virtual Author Author { get; set; }=new();
        public virtual Category Category { get; set; } = new();

    }
}
