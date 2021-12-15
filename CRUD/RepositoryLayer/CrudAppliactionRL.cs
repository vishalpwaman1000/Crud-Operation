using CRUD.CommonLayer.Models;
using CRUD.CommonUtility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.RepositoryLayer
{
    public class CrudAppliactionRL : ICrudAppliactionRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;
        int ConnectionTimeOut = 180;
        public CrudAppliactionRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection( _configuration["ConnectionStrings:DBConnection"]);
        }

        public async Task<CreateInformationResponse> CreateInformation(CreateInformationRequest request)
        {
            CreateInformationResponse resposne = new CreateInformationResponse();
            resposne.IsSuccess = true;
            resposne.Message = "Successful";
            try
            {
                if(_sqlConnection != null)
                {
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.CreateInformationQuery, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Age", request.Age);
                        await _sqlConnection.OpenAsync();
                        int Status = await sqlCommand.ExecuteNonQueryAsync();
                        if(Status <= 0)
                        {
                            resposne.IsSuccess = false;
                            resposne.Message = "CreateInformation Not Executed";
                        }
                    }
                }

            }catch(Exception ex)
            {
                resposne.IsSuccess = false;
                resposne.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
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
                using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.ReadInformation,_sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = ConnectionTimeOut;
                    await _sqlConnection.OpenAsync();
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
            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Message : " + ex.Message;
            }
            finally
            {
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
                    using (SqlCommand sqlCommand = new SqlCommand(SqlQueries.UpdateInformation, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                        sqlCommand.Parameters.AddWithValue("@Age", request.Age);
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
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, _sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeOut;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        await _sqlConnection.OpenAsync();
                        using (SqlDataReader _sqlDataReader = await sqlCommand.ExecuteReaderAsync())
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
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
            }

            return resposne;
        }

    }
}
