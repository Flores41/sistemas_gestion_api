using Abstraction.IAplication.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
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

        public ProvidersController
         (
            IProvidersAplication _IProvidersAplication,
            IConfiguration IConfiguration
            )
        {
            iIProvidersAplication = _IProvidersAplication;
            iIConfiguration = IConfiguration;
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

    }

}
