using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Domain.Models
{
    public class User : Base<int>
    {
        [MaxLength(50, ErrorMessage = "Name should not exceed 50 characters")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50,ErrorMessage ="Username should not exceed 50 characters")]

        [DisplayName("User Name")]
        public string UserName { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage = "Role should not exceed 50 characters")]
        public string Role { get; set; } = string.Empty;
        [MaxLength(100, ErrorMessage = "Password should not exceed 100 characters")]
        public string? Password { get; set; } = string.Empty;
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
