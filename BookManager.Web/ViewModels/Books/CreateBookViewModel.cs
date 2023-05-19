using BookManager.Domain.Models;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManager.Web.ViewModels.Books
{
    public class CreateBookViewModel
    {

        
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }     
       
        [DisplayName("Total Pages")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid total pages")]
        public int TotalPage { get; set; }
        
        [DisplayName("Published Date")]
        public DateTime PublishedDate{ get; set; }
        
        [DisplayName("Author")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select Author")]
        public int AuthorId { get; set; }
        public IEnumerable<Author>? AuthorList { get; set; }


        [DisplayName("Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select Category")]
        public int CategoryId { get; set; }
        public IEnumerable<Category>? CategoryList { get; set; }
    }
}
