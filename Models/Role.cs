using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Role_Name { get; set; }
        public List<User> User { get; set; }
    }
}
