using BookManager.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Web.ViewModels.Users
{
    public class EditUserViewModel:Base<int>
    {
        public string Name { get; set; } = string.Empty;
        [DisplayName("User Name")]
        public string UserName { get; set; }= string.Empty;
        [MaxLength(50, ErrorMessage = "Role should not exceed 50 characters")]
        public string Role { get; set; } = string.Empty;
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
