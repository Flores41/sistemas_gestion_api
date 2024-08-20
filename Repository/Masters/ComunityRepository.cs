using Abstraction.IRepository.Administration;
using Abstraction.IRepository.Masters;
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
using Models.Masters;
using System.Data.SqlClient;

namespace Repository.Masters
{
    public class ComunityRepository : BaseRepository, IComunityRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public ComunityRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }


        public async Task<ResultDTO<ComunityDto>> GetListComunity(ComunityDto request)
        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            List<ComunityDto> list = new List<ComunityDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_comunity", request.iid_comunity);
                parameters.Add("@p_vdescription_comunity", request.vdescription_comunity);
                parameters.Add("@p_vname_comunity", request.vname_comunity);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<ComunityDto>)await cn.QueryAsync<ComunityDto>("[dbo].[SP_COMUNITYS_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<ComunityDto>> RegisterComunity(ComunityDto request)
        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_comunity", request.iid_comunity);
                    parameters.Add("@p_vname_comunity", request.vname_comunity);
                    parameters.Add("@p_vdescription_comunity", request.vdescription_comunity);
                    //parameters.Add("@p_vurl_comunity", request.vurl_web_comunity);
                    //parameters.Add("@p_vicon_comunity", request.vicon_comunity);
                    parameters.Add("@p_vfirebase_url_comunity", request.vfirebase_url_comunity);
     
                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_COMUNITYS_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_comunity"].ToString());
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

        public async Task<ResultDTO<ComunityDto>> DeleteComunity(ComunityDto request)
        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_comunity", request.iid_comunity);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_COMUNITYS_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_comunity"].ToString());
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
