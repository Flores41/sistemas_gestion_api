using Abstraction.IAplication.Administration;
using Aplication.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Model;
using Models.Administration;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Administration
{
    [Route("Administration/ModuleOption/")]
    [ApiController]
    [Authorize]

    public class ModuleOptionController : Controller
    {
        private IModuleOptionAplication iIModuleOptionAplication;
        private IConfiguration iIConfiguration;

        public ModuleOptionController
         (IModuleOptionAplication _IModuleOptionAplication,
          IConfiguration IConfiguration)
        {
            this.iIModuleOptionAplication = _IModuleOptionAplication;
            this.iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListModule")]
        public async Task<ActionResult> GetListModule([FromBody] ModuleDto request)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            try
            {

                res = await this.iIModuleOptionAplication.GetListModule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterModule")]
        public async Task<ActionResult> RegisterModule([FromBody] ModuleDto request)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIModuleOptionAplication.RegisterModule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteModule")]
        public async Task<ActionResult> DeleteModule([FromQuery] int iid_module)
        {
            ResultDTO<ModuleDto> res = new ResultDTO<ModuleDto>();
            try
            {
                ModuleDto request = new ModuleDto();
                request.iid_module = iid_module;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIModuleOptionAplication.DeleteModule(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListOption")]
        public async Task<ActionResult> GetListOption([FromBody] OptionDto request)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            try
            {

                res = await this.iIModuleOptionAplication.GetListOption(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterOption")]
        public async Task<ActionResult> RegisterOption([FromBody] OptionDto request)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            try
            {

                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIModuleOptionAplication.RegisterOption(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteOption")]
        public async Task<ActionResult> DeleteOption([FromQuery] int iid_option)
        {
            ResultDTO<OptionDto> res = new ResultDTO<OptionDto>();
            try
            {
                OptionDto request = new OptionDto();
                request.iid_option = iid_option;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIModuleOptionAplication.DeleteOption(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }




        [HttpPost]
        [Route("GetListOptionsByModule")]
        public async Task<ActionResult> GetListOptionsByModule([FromBody] OptionByModuleDto request)
        {
            ResultDTO<OptionByModuleDto> res = new ResultDTO<OptionByModuleDto>();
            try
            {

                res = await this.iIModuleOptionAplication.GetListOptionsByModule(request);    

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
