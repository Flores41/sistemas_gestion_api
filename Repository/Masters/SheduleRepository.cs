using Abstraction.IRepository.Masters;
using Dapper;
using DataAccess.CustomConnection;
using Microsoft.Extensions.Configuration;
using Model.Util;
using Model;
using Models.Masters;
using Repositoriy.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Repository.Masters
{
    public class SheduleRepository : BaseRepository, ISheduleRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public SheduleRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }

        public async Task<ResultDTO<SheduleDto>> GetListShedule(SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            List<SheduleDto> list = new List<SheduleDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_shedule_weekly", request.iid_shedule_weekly);
                parameters.Add("@p_vname_shedule_weekly", request.vname_shedule_weekly);
                parameters.Add("@p_vdescription_shedule_weekly", request.vdescription_shedule_weekly);

                parameters.Add("@p_iid_week_shedule_weekly", request.iid_week_shedule_weekly);
                parameters.Add("@p_iid_day_shedule_weekly", request.iid_day_shedule_weekly);
                parameters.Add("@p_iid_hour_shedule_weekly", request.vhour_shedule_weekly);
                parameters.Add("@p_itype_stream", request.itype_stream);
                parameters.Add("@p_iid_user", request.iid_user);
                parameters.Add("@p_iid_comunity", request.iid_comunity);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<SheduleDto>)await cn.QueryAsync<SheduleDto>("[dbo].[SP_SHEDULE_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<SheduleDto>> RegisterShedule(SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_shedule_weekly", request.iid_shedule_weekly);
                    parameters.Add("@p_vname_shedule_weekly", request.vname_shedule_weekly);
                    parameters.Add("@p_vdescription_shedule_weekly", request.vdescription_shedule_weekly);

                    parameters.Add("@p_iid_week_shedule_weekly", request.iid_week_shedule_weekly);

                    
                    parameters.Add("@p_iid_day_shedule_weekly", request.iid_day_shedule_weekly);
                    parameters.Add("@p_iid_hour_shedule_weekly", request.vhour_shedule_weekly);
                    parameters.Add("@p_itype_stream", request.itype_stream);
                    parameters.Add("@p_iid_user", request.iid_user);
                    parameters.Add("@p_iid_comunity", request.iid_comunity);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);
              
                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_SHEDULE_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_shedule_weekly"].ToString());
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

        public async Task<ResultDTO<SheduleDto>> DeleteShedule(SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_shedule_weekly", request.iid_day_shedule_weekly);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);
             
                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_SHEDULE_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_shedule_weekly"].ToString());
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

        public async Task<ResultDTO<SheduleDto>> DeleteSheduleAll(SheduleDto request)
        {
            ResultDTO<SheduleDto> res = new ResultDTO<SheduleDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_shedule_weekly", request.iid_day_shedule_weekly);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_SHEDULE_DELETE_ALL]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_shedule_weekly"].ToString());
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
