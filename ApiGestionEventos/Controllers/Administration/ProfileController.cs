using Abstraction.IAplication.Administration;
using Aplication.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Administration
{
    [Route("Administration/Profiles/")]
    [ApiController]
    [Authorize]

    public class ProfileController : Controller
    {
        private IProfileAplication iIProfileAplication;
        private IConfiguration iIConfiguration;

        public ProfileController
         (
            IProfileAplication _IProfileAplication,
          IConfiguration IConfiguration)
        {
            this.iIProfileAplication = _IProfileAplication;
            this.iIConfiguration = IConfiguration;
        }



        [HttpPost]
        [Route("GetListProfile")]
        public async Task<ActionResult> GetListProfile([FromBody] ProfileDTO request)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            try
            {

                res = await this.iIProfileAplication.GetListProfile(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterProfile")]
        public async Task<ActionResult> RegisterProfile([FromBody] ProfileDTO request)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIProfileAplication.RegisterProfile(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteProfile")]
        public async Task<ActionResult> DeleteProfile([FromQuery] int iid_profile)
        {
            ResultDTO<ProfileDTO> res = new ResultDTO<ProfileDTO>();
            try
            {
                ProfileDTO request = new ProfileDTO();
                request.iid_profile = iid_profile;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIProfileAplication.DeleteProfile(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterProfileOption")]
        public async Task<ActionResult> RegisterProfileOption([FromBody] ProfileAccessRegisterDTO request)
        {
            ResultDTO<ProfileAccessRegisterDTO> res = new ResultDTO<ProfileAccessRegisterDTO>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                ProfileDTO profile = new ProfileDTO
                {
                    iid_profile = request.iid_profile,
                    vname_profile = request.vname_profile,
                    vdescription_profile = request.vdescription_profile,
                    istate_record = request.istate_record,
                    iid_user_token = request.iid_user_token
                };

                ResultDTO<ProfileDTO> resProfile = await this.iIProfileAplication.RegisterProfile(profile);

                if(resProfile.IsSuccess)
                {
                    ResultDTO<ProfileAccessDTO> resProfileAccess = new ResultDTO<ProfileAccessDTO>();
                    foreach (ProfileAccessDTO item in request.lstOptions)
                    {
                        item.iid_profile = resProfile.Code;
                        item.iid_profile_access = item.iid_profile_access==null ? 0 : item.iid_profile_access ;
                        item.istate_record = request.istate_record;

                        resProfileAccess = await this.iIProfileAplication.RegisterProfileAccess(item);
                    }
                }

                res.IsSuccess = resProfile.IsSuccess;
                res.Informacion = resProfile.Informacion;
                res.InnerException = resProfile.InnerException;
                res.MessageExeption = resProfile.MessageExeption;
                res.Message = resProfile.Message;


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }




        [HttpGet]
        [Route("GetListProfileAccessByProfile")]
        public async Task<ActionResult> GetListProfileAccessByProfile([FromQuery] int iid_profile)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                ProfileAccessDTO request = new ProfileAccessDTO
                {
                    iid_profile = iid_profile,
                    iindex = 0,
                    ilimit = 100000
                };
                

                res = await this.iIProfileAplication.GetListProfileAccess(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListProfileAccess")]
        public async Task<ActionResult> GetListProfileAccess([FromBody] ProfileAccessDTO request)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIProfileAplication.GetListProfileAccess(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterProfileAccess")]
        public async Task<ActionResult> RegisterProfileAccess([FromBody] ProfileAccessDTO request)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIProfileAplication.RegisterProfileAccess(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteProfileAccess")]
        public async Task<ActionResult> DeleteProfileAccess([FromQuery] int iid_profile_access)
        {
            ResultDTO<ProfileAccessDTO> res = new ResultDTO<ProfileAccessDTO>();
            try
            {
                ProfileAccessDTO request = new ProfileAccessDTO();
                request.iid_profile_access = iid_profile_access;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIProfileAplication.DeleteProfileAccess(request);

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
