using Abstraction.IRepository.Administration;
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
using Abstraction.IRepository.Masters;
using System.Data.SqlClient;

namespace Repository.Masters
{

    public class EventsRepository : BaseRepository, IEventsRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public EventsRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }


        public async Task<ResultDTO<EventsDto>> GetListEvents(EventsDto request)
        {
            ResultDTO<EventsDto> res = new ResultDTO<EventsDto>();
            List<EventsDto> list = new List<EventsDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_event", request.iid_event);
                parameters.Add("@p_vname_event", request.vname_event);
                parameters.Add("@p_iid_type_event", request.iid_type_event);
                parameters.Add("@p_iid_location", request.vlocation_event);
                parameters.Add("@p_iid_state_event", request.istate_event);
                parameters.Add("@p_iid_service", request.iid_service_event);
                parameters.Add("@p_iid_client", request.iid_client_event);
                parameters.Add("@p_iid_user_charge", request.iid_user_in_charge_event);
                parameters.Add("@p_iid_priority", request.iid_priority_event);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<EventsDto>)await cn.QueryAsync<EventsDto>("[dbo].[SP_EVENTS_LIST]", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ResultDTO<SaveEventsDTO>> SaveEvents(SaveEventsDTO request)
        {
            ResultDTO<SaveEventsDTO> res = new ResultDTO<SaveEventsDTO>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_event", request.iid_event);
                    parameters.Add("@p_vname_event", request.vname_event);
                    parameters.Add("@p_vdescription_event", request.vdescription_event);
                    parameters.Add("@p_date_event", request.ddate_event);


                    parameters.Add("@p_itype_event", request.itype_event);
                    parameters.Add("@p_iclient", request.iclient_event);
                    parameters.Add("@p_iuser_charge", request.iuser_in_charge_event);

                    parameters.Add("@p_vlocation", request.vlocation_event);
                    parameters.Add("@p_vreference", request.vreference_event);
                    parameters.Add("@p_ipriority", request.ipriority_event);
                    parameters.Add("@p_vdocumentation", request.vdocumentation_event);
                    parameters.Add("@p_dprice_event", request.dprice_event);

                    parameters.Add("@p_istate_event", request.istate_event);

                    parameters.Add("@p_vtype_services", request.vid_type_service_event);
                    parameters.Add("@p_iservice", request.vid_service_event);


                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_EVENTS_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_event"].ToString());
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

        public async Task<ResultDTO<EventsDto>> DeleteEvents(EventsDto request)
        {
            ResultDTO<EventsDto> res = new ResultDTO<EventsDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_event", request.iid_event);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_EVENTS_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_event"].ToString());
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
