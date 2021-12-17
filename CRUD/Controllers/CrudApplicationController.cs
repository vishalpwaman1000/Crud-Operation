using CRUD.CommonLayer.Models;
using CRUD.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudApplicationController : ControllerBase
    {
        public readonly ICrudAppliactionSL _crudApplicationSL;

        public CrudApplicationController(ICrudAppliactionSL crudAppliactionSL)
        {
            _crudApplicationSL = crudAppliactionSL;
        }

        [HttpPost]
        [Route("CreateInformation")]
        public async Task<IActionResult> CreateInformation(CreateInformationRequest request)
        {
            CreateInformationResponse response = null;
            try
            {

                response = await _crudApplicationSL.CreateInformation(request);

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("ReadInformation")]
        public async Task<IActionResult> ReadInformation()
        {
            ReadInformationResponse response = null;
            try
            {

                response = await _crudApplicationSL.ReadInformation();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateInformation")]
        public async Task<IActionResult> UpdateInformation(UpdateInformationRequest request)
        {
            UpdateInformationResponse response = null;
            try
            {

                response = await _crudApplicationSL.UpdateInformation(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("DeleteInformation")]
        public async Task<IActionResult> DeleteInformation(DeleteInformationRequest request)
        {
            DeleteInformationResponse response = null;
            try
            {

                response = await _crudApplicationSL.DeleteInformation(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("SearchInformationById")]
        public async Task<IActionResult> SearchInformationById(SearchInformationByIdRequest request)
        {
            SearchInformationByIdResponse response = null;
            try
            {

                response = await _crudApplicationSL.SearchInformationById(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }
    }
}
