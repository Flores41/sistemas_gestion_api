using Abstraction.IRepository.Administration;
using Dapper;
using DataAccess.CustomConnection;
using Microsoft.Extensions.Configuration;
using Model.Util;
using Model;
using Repositoriy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models.Administration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Administration
{
    public class UsersRepository :BaseRepository , IUsersRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public UsersRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }

        public async Task<ResultDTO<UserDto>> GetListUsers(UserDto request)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            List<UserDto> list = new List<UserDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_id_user", request.iid_user);
                parameters.Add("@p_vfirst_name", request.vfirst_name);
                parameters.Add("@p_vlast_name", request.vlast_name);
                parameters.Add("@p_vcode", request.vcode);
                parameters.Add("@p_vemail", request.vemail);
                parameters.Add("@p_itype_document", request.itype_document);
                parameters.Add("@p_inumber_document", request.inumber_document);
                parameters.Add("@p_vphone", request.vphone);
                parameters.Add("@p_idepartment", request.iid_addrres);
                parameters.Add("@p_iid_profile", request.iid_profile);
                parameters.Add("@p_vaddress", request.vaddress);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<UserDto>)await cn.QueryAsync<UserDto>("[dbo].[SP_USER_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                }

                int list_count = list.ToList().Count;

                res.IsSuccess = list_count > 0 ? true : false;
                res.Message = list_count > 0 ? MessagesRes.strInformacionEncontrada : MessagesRes.strInformacionNoEncontrada;
                res.iTotal_record = (int)(list_count > 0 ? list[0].itotal_record : 0);
                res.Data = list.ToList();
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = MessagesRes.strInformacionNoEncontrada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }

        public async Task<ResultDTO<UserByParameterDto>> GetListUserByParameter(UserByParameterDto request)
        {
            ResultDTO<UserByParameterDto> res = new ResultDTO<UserByParameterDto>();
            List<UserByParameterDto> list = new List<UserByParameterDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_user", request.iid_user);
                parameters.Add("@v_number_document", request.vnumber_document);


                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<UserByParameterDto>)await cn.QueryAsync<UserByParameterDto>("[dbo].[SP_USER_BY_PARAMETER]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    res.Item = (list.Any() == true ? (UserByParameterDto)list.FirstOrDefault() : null);
                    res.IsSuccess = (list.Any() == true ? true : false);
                    res.Message = list.Count > 0 ? MessagesRes.strInformacionEncontrada : MessagesRes.strInformacionNoEncontrada;

                }
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = MessagesRes.strInformacionNoEncontrada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }


    
        public async Task<ResultDTO<UserDto>> RegisterUser(UserDto request)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id_user", request.iid_user);
                    parameters.Add("@p_vfirst_name", request.vfirst_name);
                    parameters.Add("@p_vlast_name", request.vlast_name);
                    //parameters.Add("@p_vcode", request.vcode);
                    parameters.Add("@p_vemail", request.vemail);
                    //parameters.Add("@p_itype_document", request.itype_document);
                    //parameters.Add("@p_inumber_document", request.inumber_document);
                    parameters.Add("@p_iphone", request.vphone);
                    //parameters.Add("@p_idepartment", request.iid_department);
                    parameters.Add("@p_iid_profile", request.iid_profile);
                    //parameters.Add("@p_vaddress", request.vaddress);

                                       
                    parameters.Add("@p_istate_record", request.istate_record); 
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USER_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_user"].ToString());
                            res.IsSuccess = true;
                            res.Message = MessagesRes.strInformacionGrabada;
                        }
                    }
                    await mConnection.Complete();
                }

                res.IsSuccess = true;
                res.Message = MessagesRes.strInformacionGrabada;

            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = MessagesRes.strInformacionNoGrabada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }

        public async Task<ResultDTO<UserDto>> DeleteUser(UserDto request)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id_user", request.iid_user);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);
          
                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_USER_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_user"].ToString());
                            res.IsSuccess = true;
                            res.Message = MessagesRes.strInformacionEliminada;
                        }
                    }
                    await mConnection.Complete();
                }

                res.IsSuccess = true;
                res.Message = MessagesRes.strInformacionEliminada;

            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = MessagesRes.strInformacionNoEliminada;
                res.InnerException = e.Message.ToString();
            }
            return res;
        }


    }
}
