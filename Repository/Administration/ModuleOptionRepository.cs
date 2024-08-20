using Abstraction.IRepository.Administration;
using Dapper;
using DataAccess.CustomConnection;
using Microsoft.Extensions.Configuration;
using Model.Util;
using Model;
using Models.Administration;
using Repositoriy.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Repository.Administration
{
    public class ModuleOptionRepository : BaseRepository, IModuleOptionRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public ModuleOptionRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }



        public async Task<ResultDTO<ModuleDto>> GetListModule(ModuleDto request)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            List<ModuleDto> list = new List<ModuleDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_module", request.iid_module);
                parameters.Add("@p_vdescription_module", request.vdescription_module);
                parameters.Add("@p_vname_module", request.vname_module);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ModuleDto>)await cn.QueryAsync<ModuleDto>("[dbo].[SP_MODULE_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<ResultDTO<ModuleDto>> RegisterModule(ModuleDto request)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_module", request.iid_module);
                    parameters.Add("@p_vname_module", request.vname_module);
                    parameters.Add("@p_vdescription_module", request.vdescription_module);
                    parameters.Add("@p_vurl_module", request.vurl_module);
                    parameters.Add("@p_vicon_module", request.vicon_module);
                    parameters.Add("@p_iorder_module", request.iorder_module);
                    parameters.Add("@p_bvisible_module", request.bvisible_module);
                    parameters.Add("@p_bsub_menu_module", request.bsub_menu_module);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_MODULE_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_module"].ToString());
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
        public async Task<ResultDTO<ModuleDto>> DeleteModule(ModuleDto request)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_module", request.iid_module);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_MODULE_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_module"].ToString());
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




        public async Task<ResultDTO<OptionDto>> GetListOption (OptionDto request)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            List<OptionDto> list = new List<OptionDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_option", request.iid_option);
                parameters.Add("@p_iid_module", request.iid_module);
                parameters.Add("@p_vdescription_option", request.vdescription_option);
                parameters.Add("@p_vname_option", request.vname_option);
                parameters.Add("@p_iid_comunity", request.iid_comunity);
                
                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<OptionDto>)await cn.QueryAsync<OptionDto>("[dbo].[SP_OPTION_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<ResultDTO<OptionDto>> RegisterOption(OptionDto request)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_option", request.iid_option);
                    parameters.Add("@p_iid_module", request.iid_module);
                    parameters.Add("@p_vdescription_option", request.vdescription_option);
                    parameters.Add("@p_vname_option", request.vname_option);
                    parameters.Add("@p_vurl_option", request.vurl_option);
                    parameters.Add("@p_vicon_option", request.vicon_option);
                    parameters.Add("@p_iorder_option", request.iorder_option);
                    parameters.Add("@p_bvisible_option", request.bvisible_option);

                    parameters.Add("@p_iid_comunity", request.iid_comunity);
                    

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_OPTION_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_option"].ToString());
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
        public async Task<ResultDTO<OptionDto>> DeleteOption  (OptionDto request)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_option", request.iid_option);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_OPTION_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_option"].ToString());
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




        public async Task<ResultDTO<OptionByModuleDto>> GetListOptionsByModule(OptionByModuleDto request)
        {
            ResultDTO<OptionByModuleDto> res = new ResultDTO<OptionByModuleDto>();
            List<OptionByModuleDto> list = new List<OptionByModuleDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_option", request.iid_option);
                parameters.Add("@p_iid_module", request.iid_module);
                parameters.Add("@p_vname_module", request.vname_module);
                parameters.Add("@p_vname_option", request.vname_option);
                parameters.Add("@p_vdescription_module", request.vdescription_module);
                parameters.Add("@p_vdescription_option", request.vdescription_option);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<OptionByModuleDto>)await cn.QueryAsync<OptionByModuleDto>("[dbo].[SP_OPTION_BY_MODULE_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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


    }
}
