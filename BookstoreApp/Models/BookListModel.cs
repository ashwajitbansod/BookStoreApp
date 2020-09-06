using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookstoreApp.Models
{
    public class BookListModel
    {
        public long BookId { get; set; }
        [Required(ErrorMessage = "Please enter a book name")]
        [RegularExpression("^[a-zA-Z ',-]+$", ErrorMessage = "Digits and Special characters are not allowed.")]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Please enter a author name")]
        [RegularExpression("^[a-zA-Z ',-]+$", ErrorMessage = "Digits and Special characters are not allowed.")]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Display(Name = "Publication")]
        public string BookPublication { get; set; }

        [Display(Name = "Book Date")]
        public DateTime? BookDate { get; set; }

        [Required(ErrorMessage = "Please enter  ISBN")]
        [RegularExpression("^[0-9 ',-]+$", ErrorMessage = "Letters and Special characters are not allowed.")]
        [Display(Name = "ISBN")]
        public string BookISBN { get; set; }
        [Display(Name = "Description")]
        public string BookDescription { get; set; }
        [Display(Name = "Book Price")]
        public decimal? BookPrice { get; set; }

        public HttpPostedFileBase BookPicImage { get; set; }

        [Display(Name = "Picture")]
        public string BookPic { get; set; }
        public bool? IsDelete { get; set; }
    }
}