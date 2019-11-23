using LibraryCourse.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class Books
    {
        public int Id { get; set; }
        [Required]
        public string Book_Name { get; set; }
        [Required]
        public string Author_Name { get; set; }
        public List<Queue> Queue { get; set; }
        public List<History> History { get; set; }

        
    }
}
