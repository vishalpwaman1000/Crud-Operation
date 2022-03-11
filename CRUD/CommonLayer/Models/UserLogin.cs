using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonLayer.Models
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserInformation data { get; set; }
        public string Token { get; set; }
    }

    public class UserInformation
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
