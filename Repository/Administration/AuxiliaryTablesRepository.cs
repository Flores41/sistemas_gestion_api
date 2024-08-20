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
    public class AuxiliaryTablesRepository : BaseRepository, IAuxiliaryTablesRepository
    {
        private string _connectionString = "";
        private IConfiguration Configuration;


        public AuxiliaryTablesRepository(ICustomConnection connection,
                    IConfiguration configuration) : base(connection)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetConnectionString("CS_SistemaVentas");
        }

        #region TABLE HEADBOARD

        public async Task<ResultDTO<TableHeadBoardDto>> GetListTableHead(TableHeadBoardDto request)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            List<TableHeadBoardDto> list = new List<TableHeadBoardDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_table_headboard", request.iid_table_headboard);
                parameters.Add("@p_vdescription", request.vdescription);
                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<TableHeadBoardDto>)await cn.QueryAsync<TableHeadBoardDto>("[dbo].[SP_TABLE_HEADBOARD_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<TableHeadBoardDto>> RegisterTableHead(TableHeadBoardDto request)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_table_headboard", request.iid_table_headboard);
                    parameters.Add("@p_vdescription", request.vdescription);
                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);
        
                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLE_HEADBOARD_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_table_headboard"].ToString());
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

        public async Task<ResultDTO<TableHeadBoardDto>> DeleteTableHead(TableHeadBoardDto request)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_table_headboard", request.iid_table_headboard);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLE_HEADBOARD_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_table_headboard"].ToString());
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

        #endregion

        #region TABLE DETAIL
        public async Task<ResultDTO<TableDetailDto>> GetListTableDetail(TableDetailDto request)
        {
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            List<TableDetailDto> list = new List<TableDetailDto>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p_iid_table_detail", request.iid_table_detail);
                parameters.Add("@p_iid_table_headboard", request.iid_table_headboard);
                parameters.Add("@p_iid_detail", request.iid_detail);
                parameters.Add("@p_vdescription", request.vdescription);

                parameters.Add("@p_vvalue_text_1", request.vvalue_text_1);
                parameters.Add("@p_vvalue_text_2", request.vvalue_text_2);
                parameters.Add("@p_vvalue_text_3", request.vvalue_text_3);

                parameters.Add("@p_vvalue_integer_1", request.vvalue_integer_1);
                parameters.Add("@p_vvalue_integer_2", request.vvalue_integer_2);
                parameters.Add("@p_vvalue_integer_3", request.vvalue_integer_3);

                parameters.Add("@p_vvalue_decimal_1", request.vvalue_decimal_1);
                parameters.Add("@p_vvalue_decimal_2", request.vvalue_decimal_2);
                parameters.Add("@p_vvalue_decimal_3", request.vvalue_decimal_3);

                parameters.Add("@p_istate_record", request.istate_record);
                parameters.Add("@p_index", request.iindex);
                parameters.Add("@p_limit", request.ilimit);

                using (var cn = new SqlConnection(_connectionString))
                {
                    list = (List<TableDetailDto>)await cn.QueryAsync<TableDetailDto>("[dbo].[SP_TABLE_DETAIL_LIST]", parameters, commandType: System.Data.CommandType.StoredProcedure);
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

        public async Task<ResultDTO<TableDetailDto>> RegisterTableDetail(TableDetailDto request)
        {
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_iid_table_detail", request.iid_table_detail);
                    parameters.Add("@p_iid_table_headboard", request.iid_table_headboard);
                    parameters.Add("@p_vdescription", request.vdescription);

                    parameters.Add("@p_vvalue_text_1", request.vvalue_text_1);
                    parameters.Add("@p_vvalue_text_2", request.vvalue_text_2);
                    parameters.Add("@p_vvalue_text_3", request.vvalue_text_3);

                    parameters.Add("@p_vvalue_integer_1", request.vvalue_integer_1);
                    parameters.Add("@p_vvalue_integer_2", request.vvalue_integer_2);
                    parameters.Add("@p_vvalue_integer_3", request.vvalue_integer_3);

                    parameters.Add("@p_vvalue_decimal_1", request.vvalue_decimal_1);
                    parameters.Add("@p_vvalue_decimal_2", request.vvalue_decimal_2);
                    parameters.Add("@p_vvalue_decimal_3", request.vvalue_decimal_3);

                    parameters.Add("@p_istate_record", request.istate_record);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLE_DETAIL_REGISTER_UPDATE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_table_detail"].ToString());
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

        public async Task<ResultDTO<TableDetailDto>> DeleteTableDetail(TableDetailDto request)
        {
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            try
            {
                using (var cn = await mConnection.BeginConnection(true))
                {
                    var parameters = new DynamicParameters();
                   parameters.Add("@p_iid_table_detail", request.iid_table_detail);
                    parameters.Add("@p_iuser_aud", request.iid_user_token);

                    using (var lector = await cn.ExecuteReaderAsync("[dbo].[SP_TABLE_DETAIL_DELETE]", parameters, commandType: CommandType.StoredProcedure, transaction: mConnection.GetTransaction()))
                    {
                        while (lector.Read())
                        {
                            res.Code = Convert.ToInt32(lector["iid_table_detail"].ToString());
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

        #endregion

    }
}
