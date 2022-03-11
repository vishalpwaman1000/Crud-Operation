using CRUD.CommonLayer.Models;
using CRUD.ServiceLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] //Jwt
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]  //Cookies
    public class CrudApplicationController : ControllerBase
    {
        public readonly ICrudAppliactionSL _crudApplicationSL;

        public CrudApplicationController(ICrudAppliactionSL crudAppliactionSL)
        {
            _crudApplicationSL = crudAppliactionSL;
        }

        [HttpPost]
        [AllowAnonymous]
        //[Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            RegisterUserResponse response = null;
            try
            {

                response = await _crudApplicationSL.RegisterUser(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        //[Route("RegisterUser")]
        public async Task<IActionResult> UserLogin(UserLoginRequest request)
        {
            UserLoginResponse response = null;
            try
            {

                response = await _crudApplicationSL.UserLogin(request);
                if (response.IsSuccess)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, response.data.UserName),
                        new Claim(ClaimTypes.PrimarySid, response.data.UserId.ToString()),
                        new Claim(ClaimTypes.Role, response.data.Role),
                        new Claim("Roles", response.data.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        //[Route("CreateInformation")]
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
        [Authorize(Roles = "User")]
        //[Route("ReadInformation")]
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
        [Authorize(Roles = "User")]
        //[Route("UpdateInformation")]
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
        [Authorize(Roles = "User")]
        //[Route("DeleteInformation")]
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
        [Authorize(Roles = "User")]
        //[Route("SearchInformationById")]
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
