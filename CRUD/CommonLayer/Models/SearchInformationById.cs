using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonLayer.Models
{
    public class SearchInformationByIdRequest
    {
        public int UserId { get; set; }
    }

    public class SearchInformationByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public SearchInformationById searchInformationById { get; set; }
    }

    public class SearchInformationById
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
