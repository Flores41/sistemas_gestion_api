using Abstraction.IAplication.Masters;
using Abstraction.IAplication.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Masters;
using Models.Util;
using NPOI.SS.Formula.Functions;
using System.Net.Mail;
using System.Security.Claims;
namespace ApiGestionEventos.Controllers.Masters
{
    [Route("Masters/Providers/")]
    [ApiController]
    [Authorize]

    public class ProvidersController : Controller
    {

        private IProvidersAplication iIProvidersAplication;
        private IConfiguration iIConfiguration;
        private readonly IUtilAplication _IUtilAplication;



        public ProvidersController
         (
            IProvidersAplication _IProvidersAplication,
            IConfiguration IConfiguration,
            IUtilAplication IUtilAplication
            )
        {
            this.iIProvidersAplication = _IProvidersAplication;
            this.iIConfiguration = IConfiguration;
            this._IUtilAplication = IUtilAplication;
        }



        [HttpPost]
        [Route("GetListProviders")]
        public async Task<ActionResult> GetListProviders([FromBody] ProvidersDto request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try
            {

                res = await iIProvidersAplication.GetListProviders(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("RegisterProvider")]
        public async Task<ActionResult> RegisterProvider([FromBody] RegisterProvidersDto request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIProvidersAplication.RegisterProvider(request);


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteProvider")]
        public async Task<ActionResult> DeleteProvider([FromQuery] int iid_provider)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try
            {
                ProvidersDto request = new ProvidersDto();
                request.iid_provider = iid_provider;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIProvidersAplication.DeleteProvider(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("SendEmailProvider")]
        public async Task<ActionResult> SendEmailProvider([FromBody] EmailDTO request)
        {
            ResultDTO<ProvidersDto> res = new ResultDTO<ProvidersDto>();
            try {

                 var email = await this._IUtilAplication.envioMail(request);

                res.IsSuccess = email.IsSuccess;
                res.Message = email.Message;

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
