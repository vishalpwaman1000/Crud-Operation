using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonLayer.Models
{
    public class CreateInformationRequest
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }

    public class CreateInformationResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
