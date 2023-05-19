using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Domain.Models
{
    public class Author : Base<int>
    {

        public Author()
        {
            Books = new HashSet<Book>();
        }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<Book>? Books { get; set; }

        [Required]
        public virtual User CreatedBy { get; set; } = new();
    }
}
