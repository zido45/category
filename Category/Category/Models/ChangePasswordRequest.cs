using System;
using System.Collections.Generic;
using System.Text;

namespace Category.Models
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }

}
