using CRUD.CommonLayer.Models;
using CRUD.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.ServiceLayer
{
    public class CrudAppliactionSL : ICrudAppliactionSL
    {
        public readonly ICrudAppliactionRL _crudAppliactionRL;
        public CrudAppliactionSL(ICrudAppliactionRL crudAppliactionRL)
        {
            _crudAppliactionRL = crudAppliactionRL;
        }

        public async Task<CreateInformationResponse> CreateInformation(CreateInformationRequest request)
        {

            return await _crudAppliactionRL.CreateInformation(request);
            
        }

        public async Task<ReadInformationResponse> ReadInformation()
        {
            return await _crudAppliactionRL.ReadInformation(); 
        }

        public async Task<UpdateInformationResponse> UpdateInformation(UpdateInformationRequest request)
        {
            return await _crudAppliactionRL.UpdateInformation(request);
        }

        public async Task<DeleteInformationResponse> DeleteInformation(DeleteInformationRequest request)
        {
            return await _crudAppliactionRL.DeleteInformation(request);
        }

        public async Task<SearchInformationByIdResponse> SearchInformationById(SearchInformationByIdRequest request)
        {
            return await _crudAppliactionRL.SearchInformationById(request);
        }
    }
}
