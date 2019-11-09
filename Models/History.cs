using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class History
    {
        public int Id { get; set; }

        public int CardId { get; set; }
        [ForeignKey("CardId")]
        public Library_Card Library_Card { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Books Books { get; set; }
    }
}
