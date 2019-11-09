using LibraryCourse.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class Books : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string Book_Name { get; set; }
        [Required]
        [ValidAuthorName(allowedName: "J. K. Rowling", ErrorMessage ="Author name must be J. K. Rowling")]
        public string Author_Name { get; set; }
        public List<Queue> Queue { get; set; }
        public List<History> History { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Book_Name.Length > 10)
            {
                yield return new ValidationResult(
                    $"Book name contains too much characters",
                    new []{ "Book_Name" });

            }
        }
    }
}
