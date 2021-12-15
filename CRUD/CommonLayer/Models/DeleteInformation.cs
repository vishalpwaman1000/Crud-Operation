using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonLayer.Models
{
    public class DeleteInformationRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteInformationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DeleteInformation deleteInformation { get; set; }
    }

    public class DeleteInformation
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
