using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Remote(action: "IsLoginInUse", controller:"User")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage ="Too short password, should be more than 5")]
        public string Password { get; set; }
        [Required]
        public string Full_Name { get; set; }
        public Role User_Role { get; set; }
        public Requests User_Requests { get; set; }
        public Library_Card Library_Card { get; set; }

        
    }
}
