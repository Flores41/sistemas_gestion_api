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
    public class ServicesRepository : BaseRepository, IServicesRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public ServicesRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }


        public async Task<ResultDTO<ServicesDto>> GetListServices(ServicesDto request)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            List<ServicesDto> list = new List<ServicesDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_Service", request.iid_service);
                parameters.Add("@p_vname_service", request.vname_service);
                parameters.Add("@p_iprovider_service", request.iid_provider_service);
                parameters.Add("@p_type_price_service", request.vtype_price_service);
                parameters.Add("@p_price_service", request.vprice_service);
                parameters.Add("@p_type_events_service", request.ilst_type_events_service);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ServicesDto>)await cn.QueryAsync<ServicesDto>("[dbo].[SP_SERVICE_LIST]", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ResultDTO<ServicesDto>> RegisterService(ServicesRegisterDto request)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_Service", request.iid_service);
                    parameters.Add("@p_vname_service", request.vname_service);
                    parameters.Add("@p_vdescription_service", request.vdescription_service);

                    parameters.Add("@p_iprovider_service", request.iid_provider_service);

                    parameters.Add("@p_type_price_service", request.itype_price_service);
                    parameters.Add("@p_price_service", request.vprice_service);

                    parameters.Add("@p_type_events_service", request.ilst_type_events_service);

                    parameters.Add("@p_references_service", request.lst_references_service);
                    parameters.Add("@p_quantity_service", request.iquantity_service);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_SERVICE_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_service"].ToString());
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

        public async Task<ResultDTO<ServicesDto>> DeleteService(ServicesDto request)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_service", request.iid_service);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_SERVICE_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_service"].ToString());
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
