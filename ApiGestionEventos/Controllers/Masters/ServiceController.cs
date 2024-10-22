using Abstraction.IAplication.Masters;
using Abstraction.IAplication.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Masters;
using Models.Util;
using System.Security.Claims;

namespace ApiGestionEventos.Controllers.Masters
{
    [Route("Masters/Services/")]
    [ApiController]
    [Authorize]

    public class ServicesController : Controller
    {

        private IServicesAplication iIServicesAplication;
        private IConfiguration iIConfiguration;
        private readonly IUtilAplication _IUtilAplication;



        public ServicesController
         (
            IServicesAplication _IServicesAplication,
            IConfiguration IConfiguration,
            IUtilAplication IUtilAplication
            )
        {
            this.iIServicesAplication = _IServicesAplication;
            this.iIConfiguration = IConfiguration;
            this._IUtilAplication = IUtilAplication;
        }



        [HttpPost]
        [Route("GetListServices")]
        public async Task<ActionResult> GetListServices([FromBody] ServicesDto request)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            try
            {

                res = await iIServicesAplication.GetListServices(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("RegisterService")]
        public async Task<ActionResult> RegisterService([FromBody] ServicesRegisterDto request)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIServicesAplication.RegisterService(request);


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteService")]
        public async Task<ActionResult> DeleteService([FromQuery] int iid_service)
        {
            ResultDTO<ServicesDto> res = new ResultDTO<ServicesDto>();
            try
            {
                ServicesDto request = new ServicesDto();
                request.iid_service = iid_service;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIServicesAplication.DeleteService(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }




    }

}
