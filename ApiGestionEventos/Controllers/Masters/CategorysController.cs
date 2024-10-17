using Abstraction.IAplication.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
using System.Security.Claims;

namespace ApiGestionEventos.Controllers.Masters
{
    [Route("Masters/Categorys/")]
    [ApiController]
    [Authorize]

    public class CategorysController : Controller
    {
        private ICategorysAplication iICategorysAplication;
        private IConfiguration iIConfiguration;

        public CategorysController
         (
            ICategorysAplication _ICategorysAplication,
            IConfiguration IConfiguration
            )
        {
            iICategorysAplication = _ICategorysAplication;
            iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListCategorys")]
        public async Task<ActionResult> GetListCategorys([FromBody] CategorysDto request)
        {
            ResultDTO<CategorysDto> res = new ResultDTO<CategorysDto>();
            try
            {

                res = await iICategorysAplication.GetListCategorys(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("RegisterCategory")]
        public async Task<ActionResult> RegisterCategory([FromBody] RegisterCategorysDto request)
        {
            ResultDTO<CategorysDto> res = new ResultDTO<CategorysDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iICategorysAplication.RegisterCategory(request);


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory([FromQuery] int iid_category)
        {
            ResultDTO<CategorysDto> res = new ResultDTO<CategorysDto>();
            try
            {
                CategorysDto request = new CategorysDto();
                request.iid_category = iid_category;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iICategorysAplication.DeleteCategory(request);

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
