using Abstraction.IRepository.Administration;
using Dapper;
using DataAccess.CustomConnection;
using Microsoft.Extensions.Configuration;
using Model.Util;
using Model;
using Repositoriy.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction.IRepository.Masters;
using System.Data.SqlClient;
using Models.Masters;

namespace Repository.Masters
{
    public class ProvidersRepository : BaseRepository, IProvidersRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public ProvidersRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }


        public async Task<ResultDTO<ProvidersDto>> GetListProviders(ProvidersDto request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            List<ProvidersDto> list = new List<ProvidersDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_provider", request.iid_provider);
                parameters.Add("@p_vrazon_social_provider", request.vrazon_social_provider);
                parameters.Add("@p_vruc_provider", request.vruc_provider);
                parameters.Add("@p_vtype_provider", request.vtype_provider);
                parameters.Add("@p_vphone_number_provider", request.vphone_number_provider);
                parameters.Add("@p_vemail_provider", request.vemail_provider);
                parameters.Add("@p_vweb_address_provider", request.vweb_address_provider);
                parameters.Add("@p_irating_provider", request.irating_provider);
                parameters.Add("@p_iubigeo_provider", request.iubigeo_provider);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ProvidersDto>)await cn.QueryAsync<ProvidersDto>("[dbo].[SP_PROVIDER_LIST]", parameters, commandType: CommandType.StoredProcedure);
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

        public async Task<ResultDTO<ProvidersDto>> RegisterProvider(RegisterProvidersDto request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_provider", request.iid_provider);
                    parameters.Add("@p_vrazon_social_provider", request.vrazon_social_provider);
                    parameters.Add("@p_vruc_provider", request.vruc_provider);
                    parameters.Add("@p_vtype_provider", request.vtype_provider);
                    parameters.Add("@p_vphone_number_provider", request.vphone_number_provider);
                    parameters.Add("@p_vemail_provider", request.vemail_provider);
                    parameters.Add("@p_vweb_address_provider", request.vweb_address_provider);
                    parameters.Add("@p_irating_provider", request.irating_provider);
                    parameters.Add("@p_iubigeo_provider", request.iubigeo_provider);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROVIDER_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_provider"].ToString());
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

        public async Task<ResultDTO<ProvidersDto>> DeleteProvider(ProvidersDto request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_provider", request.iid_provider);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_PROVIDER_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_provider"].ToString());
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
