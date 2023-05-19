using Microsoft.Build.Framework;
using System.ComponentModel;

namespace BookManager.Web.ViewModels.Users
{
    public class UserLogin
    {
        [DisplayName("User Name")]
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
