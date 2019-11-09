using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class Library_Card
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime ExpirationDate { get; set; }

        public List<Queue> Queue { get; set; }
        public List<History> History { get; set; }

    }
}
