using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonLayer.Models
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Role Is Required")]
        public string Role { get; set; }
    }

    public class RegisterUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
