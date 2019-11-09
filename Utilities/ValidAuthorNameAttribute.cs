using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCourse.Utilities
{
    public class ValidAuthorNameAttribute : ValidationAttribute
    {
        private readonly string allowedName;
        public ValidAuthorNameAttribute(string allowedName)
        {
            this.allowedName = allowedName;
        }
        public override bool IsValid(object value)
        {
            string a_name = value.ToString();
            return a_name.ToUpper() == allowedName.ToUpper();
        }
    }
}
