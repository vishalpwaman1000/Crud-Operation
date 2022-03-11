using CRUD.CommonLayer.Models;
using CRUD.CommonUtility;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.RepositoryLayer
{
    public class CrudAppliactionRL : ICrudAppliactionRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        public readonly MySqlConnection _mySqlConnection;
        int ConnectionTimeOut = 180;
        public CrudAppliactionRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration["ConnectionStrings:SqlServerDBConnection"]);
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {

            RegisterUserResponse resposne = new RegisterUserResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {

                    if(_sqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        await _sqlConnection.OpenAsync();
                    }

                    if(request.Password != request.ConfirmPassword)
                    {
                        resposne.IsSuccess = false;
                        resposne.Message = "Password Not Match";
                        return resposne;
                    }

                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.RegisterUser, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                        sqlCommand.Parameters.AddWithValue("@Role", request.Role);
                        //await _mySqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "RegisterUser Query Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;

        }

        public async Task<UserLoginResponse> UserLogin(UserLoginRequest request)
        {
            UserLoginResponse response = new UserLoginResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if (_sqlConnection != null)
                {

                    if (_sqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        await _sqlConnection.OpenAsync();
                    }

                   
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.UserLogin, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                        //await _mySqlConnection.OpenAsync();
                        using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.data = new UserInformation();
                                response.Message = "Successfully Login";
                                await dataReader.ReadAsync();
                                response.data.UserId = dataReader["UserId"] != DBNull.Value ? Convert.ToInt32(dataReader["UserId"]) :0;
                                response.data.UserName = dataReader["UserName"] != DBNull.Value ? Convert.ToString(dataReader["UserName"]) : string.Empty;
                                response.data.Role = dataReader["Role"] != DBNull.Value ? Convert.ToString(dataReader["Role"]) : string.Empty;
                                response.Token = GenerateJWT(response.data.UserId.ToString(), response.data.UserName, response.data.Role);
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "Login Failed";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<CreateInformationResponse> CreateInformation(CreateInformationRequest request)
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                //if (_mySqlConnection != null)
                if(_sqlConnection != null)
                {
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.CreateInformationQuery, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.CreateInformationQuery, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }

        public async Task<ReadInformationResponse> ReadInformation()
        {
            ReadInformationResponse response = new ReadInformationResponse();
            response.readInformation = new List<ReadInformation>();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadInformation, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.ReadInformation,_sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using(SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            while (await _sqlDataReader.ReadAsync())
                            {
                                ReadInformation getResponse = new ReadInformation();
                                getResponse.UserID = _sqlDataReader["ID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ID"]) : 0;
                                getResponse.UserName = _sqlDataReader["UserName"] != DBNull.Value ? _sqlDataReader["UserName"].ToString() : string.Empty;
                                getResponse.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;
                                response.readInformation.Add(getResponse);
                            }
                        }
                        else
                        {
                            response.Message = "No data Return";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<UpdateInformationResponse> UpdateInformation(UpdateInformationRequest request)
        {
            UpdateInformationResponse resposne = new UpdateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateInformation, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.UpdateInformation, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "Information Not Update";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }

        public async Task<DeleteInformationResponse> DeleteInformation(DeleteInformationRequest request)
        {
            DeleteInformationResponse resposne = new DeleteInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if (_sqlConnection != null)
                {
                    string StoreProcedure = "SP_DeleteInformation";
                    string SqlQuery = SqlQueries.DeleteInformation;
                    //using (MySqlCommand sqlCommand = new MySqlCommand(StoreProcedure, _mySqlConnection))
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        //sqlCommand.Parameters.AddWithValue("?UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        //await _mySqlConnection.OpenAsync();
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if (Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "UnSuccessful";
                        }
                       /* using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (_sqlDataReader.HasRows)
                            {
                                await _sqlDataReader.ReadAsync();
                                resposne.deleteInformation = new DeleteInformation();
                                resposne.deleteInformation.UserID = _sqlDataReader["ID"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["ID"]) : 0;
                                resposne.deleteInformation.UserName = _sqlDataReader["UserName"] != DBNull.Value ? _sqlDataReader["UserName"].ToString() : string.Empty;
                                resposne.deleteInformation.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;
                            }
                            else
                            {
                                resposne.Message = "User Not Found";
                            }
                        }*/
                    }
                }

            }
            catch (Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }

        public async Task<SearchInformationByIdResponse> SearchInformationById(SearchInformationByIdRequest request)
        {
            SearchInformationByIdResponse response = new SearchInformationByIdResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                //using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.SearchInformationById, _mySqlConnection))
                using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.ReadInformation,_sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                    //await _mySqlConnection.OpenAsync();
                    await _sqlConnection.OpenAsync();
                    //using (DbDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    using(SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (_sqlDataReader.HasRows)
                        {
                            await _sqlDataReader.ReadAsync();
                            response.searchInformationById = new SearchInformationById();
                            response.searchInformationById.UserName = _sqlDataReader["UserName"] != DBNull.Value ? _sqlDataReader["UserName"].ToString() : string.Empty;
                            response.searchInformationById.Age = _sqlDataReader["Age"] != DBNull.Value ? Convert.ToInt32(_sqlDataReader["Age"]) : 0;


                        }
                        else
                        {
                            response.Message = "No data Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
                //await _mySqlConnection.CloseAsync();
                //await _mySqlConnection.DisposeAsync();
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return response;
        }

        public string GenerateJWT(string UserId, string Email, string Role)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //claim is used to add identity to JWT token
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sid, UserId),
                 new Claim(JwtRegisteredClaimNames.Email, Email),
                 new Claim("Roles", Role),
                 new Claim(ClaimTypes.Role,Role),
                 new Claim("Date", DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audiance"],
              claims,    //null original value
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            string Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token 
            return Data;
        }


    }
}
