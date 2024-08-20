using Abstraction.IRepository.Administration;
using Azure.Core;
using Dapper;
using DataAccess.CustomConnection;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Util;
using Models.Administration;
using Repositoriy.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Administration
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public ProfileRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }


        public async Task<ResultDTO<ProfileDTO>> GetListProfile(ProfileDTO request)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            List<ProfileDTO> list = new List<ProfileDTO>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_profile", request.iid_profile);
                parameters.Add("@p_vname_profile", request.vname_profile);
                parameters.Add("@p_vdescription_profile", request.vdescription_profile);
               
                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ProfileDTO>)await cn.QueryAsync<ProfileDTO>("[dbo].[SP_PROFILE_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<ProfileDTO>> RegisterProfile(ProfileDTO request)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_profile", request.iid_profile);
                    parameters.Add("@p_vname_profile", request.vname_profile);
                    parameters.Add("@p_vdescription_profile", request.vdescription_profile);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROFILE_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_profile"].ToString());
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

        public async Task<ResultDTO<ProfileDTO>> DeleteProfile(ProfileDTO request)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_profile", request.iid_profile);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROFILE_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_profile"].ToString());
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





        public async Task<ResultDTO<ProfileAccessDTO>> GetListProfileAccess(ProfileAccessDTO request)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            List<ProfileAccessDTO> list = new List<ProfileAccessDTO>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_profile", request.iid_profile);

                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ProfileAccessDTO>)await cn.QueryAsync<ProfileAccessDTO>("[dbo].[SP_PROFILE_ACCESS_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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


        public async Task<ResultDTO<ProfileAccessDTO>> RegisterProfileAccess(ProfileAccessDTO request)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_profile_access", request.iid_profile_access);                    
                    parameters.Add("@p_iid_profile", request.iid_profile);
                    parameters.Add("@p_iid_module", request.iid_module);
                    parameters.Add("@p_iid_option", request.iid_option);

                    parameters.Add("@p_baccess_view", request.baccess_view);
                    parameters.Add("@p_baccess_create", request.baccess_create);
                    parameters.Add("@p_baccess_update", request.baccess_update);
                    parameters.Add("@p_baccess_delete", request.baccess_delete);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROFILE_ACCESS_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_profile_access"].ToString());
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

        public async Task<ResultDTO<ProfileAccessDTO>> DeleteProfileAccess(ProfileAccessDTO request)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_profile_access", request.iid_profile_access);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROFILE_ACCESS_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_profile_access"].ToString());
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
