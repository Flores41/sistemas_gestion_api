using Abstraction.IAplication.Administration;
using BudgetForcast.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
using System.Collections.Generic;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Administration
{
    [Route("Administration/AuxiliaryTables/")]
    [ApiController]
    [Authorize]

    public class AuxiliaryTablesController : Controller
    {
        private IAuxiliaryTablesAplication iIAuxiliaryTablesAplication;
        private IConfiguration iIConfiguration;

        public AuxiliaryTablesController
         (
            IAuxiliaryTablesAplication _IAuxiliaryTablesAplication,
            IConfiguration IConfiguration
            )
        {
            this.iIAuxiliaryTablesAplication = _IAuxiliaryTablesAplication;
            this.iIConfiguration = IConfiguration;
        }


        #region TABLE HEADBOARD

        [HttpPost]
        [Route("GetListTableHead")]
        public async Task<ActionResult> GetListTableHead([FromBody] TableHeadBoardDto request)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            try
            {

                res = await this.iIAuxiliaryTablesAplication.GetListTableHead(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterTableHead")]
        public async Task<ActionResult> RegisterTableHead([FromBody] TableHeadBoardDto request)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIAuxiliaryTablesAplication.RegisterTableHead(request);                

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteTableHead")]
        public async Task<ActionResult> DeleteTableHead([FromQuery] int iid_table_headboard)
        {
            ResultDTO<TableHeadBoardDto> res = new ResultDTO<TableHeadBoardDto>();
            try
            {               
                TableHeadBoardDto request  = new TableHeadBoardDto();
                request.iid_table_headboard = iid_table_headboard;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIAuxiliaryTablesAplication.DeleteTableHead(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        #endregion



        #region TABLE DETAIL

        [HttpPost]
        [Route("GetListTableDetail")]
        public async Task<ActionResult> GetListTableDetail([FromBody] TableDetailDto request)
        {
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            try
            {

                res = await this.iIAuxiliaryTablesAplication.GetListTableDetail(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterTableDetail")]
        public async Task<ActionResult> RegisterTableDetail([FromBody] TableDetailDto request)
        {
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


                res = await this.iIAuxiliaryTablesAplication.RegisterTableDetail(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteTableDetail")]
        public async Task<ActionResult> DeleteTableDetail([FromQuery] int iid_table_detail)
        {
            
            ResultDTO<TableDetailDto> res = new ResultDTO<TableDetailDto>();
            try
            {
                
                TableDetailDto request = new TableDetailDto();
                request.iid_table_detail = iid_table_detail;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIAuxiliaryTablesAplication.DeleteTableDetail(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpGet]
        [Route("GetListTableDetailCB")]
        public async Task<ActionResult> GetListTableDetailCB([FromQuery] int iid_table_headboard)
        {
            ResultDTO<ListCbDTO> res = new ResultDTO<ListCbDTO>();

            try
            {

                TableDetailDto request = new TableDetailDto();
                request.iid_table_detail = -1;
                request.iid_table_headboard = iid_table_headboard;
                request.iid_detail = -1;
                request.vdescription = "";

                request.vvalue_text_1 = "";
                request.vvalue_text_2 = "";
                request.vvalue_text_3 = "";

                request.vvalue_integer_1 = -1;
                request.vvalue_integer_2 = -1;
                request.vvalue_integer_3 = -1;

                request.vvalue_decimal_1 = -1;
                request.vvalue_decimal_2 = -1;
                request.vvalue_decimal_3 = -1;

                request.istate_record = 1;
                request.iindex = 0;
                request.ilimit = 1000;

                ResultDTO<TableDetailDto> _res = await this.iIAuxiliaryTablesAplication.GetListTableDetail(request);

                if (_res.IsSuccess)
                {
                    List<ListCbDTO> _list = _res.Data.Select( p => new ListCbDTO
                    {
                        id      = p.iid_detail,
                        vvalue1 = p.vvalue_text_1,
                        vvalue2 = p.vvalue_text_2,
                        vvalue3 = p.vvalue_text_3,
                        ivalue1 = p.vvalue_integer_1,
                        ivalue2 = p.vvalue_integer_2,
                        ivalue3 = p.vvalue_integer_3,
                        dvalue1 = p.vvalue_decimal_1,
                        dvalue2 = p.vvalue_decimal_2,
                        dvalue3 = p.vvalue_decimal_3,
                    }).ToList();

           
                    res.IsSuccess = _res.IsSuccess;
                    res.iTotal_record = _res.iTotal_record;
                    res.Data = _list.ToList();
                }
                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        #endregion

        #region UBIGEO
        [HttpPost]
        [Route("GetListUbigeo")]
        public async Task<ActionResult> GetListUbigeo([FromBody] UbigeoDto request)
        {
            ResultDTO<UbigeoDto> res = new ResultDTO<UbigeoDto>();
            try
            {

                res = await this.iIAuxiliaryTablesAplication.GetListUbigeo(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        #endregion
    }
}
