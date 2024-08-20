using Abstraction.IAplication.Administration;
using Abstraction.IAplication.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Masters;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Masters
{
    [Route("Masters/Range/")]
    [ApiController]
    [Authorize]

    public class RangeController : Controller
    {
        private IRangeAplication iIRangeAplication;
        private IModuleOptionAplication iIModuleOptionAplication;

        private IConfiguration iIConfiguration;

        public RangeController
         (
            IRangeAplication _IRangeAplication,
            IModuleOptionAplication _IModuleOptionAplication,
            IConfiguration IConfiguration)
        {
            this.iIRangeAplication = _IRangeAplication;
            this.iIModuleOptionAplication = _IModuleOptionAplication;

            this.iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListRange")]
        public async Task<ActionResult> GetListRange([FromBody] RangeDto request)
        {
            ResultDTO<RangeDto> res = new ResultDTO<RangeDto>();
            try
            {

                res = await this.iIRangeAplication.GetListRange(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterRange")]
        public async Task<ActionResult> RegisterRange([FromBody] RangeDto request)
        {
            ResultDTO<RangeDto> res = new ResultDTO<RangeDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIRangeAplication.RegisterRange(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteRange")]
        public async Task<ActionResult> DeleteRange([FromQuery] int iid_range)
        {
            ResultDTO<RangeDto> res = new ResultDTO<RangeDto>();
            try
            {
                RangeDto request = new RangeDto();
                request.iid_range = iid_range;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIRangeAplication.DeleteRange(request);

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
