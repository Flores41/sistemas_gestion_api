using Abstraction.IAplication.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Administration
{
    [Route("Administration/Users/")]
    [ApiController]
    [Authorize]

    public class UsersController : Controller
    {
        private IUsersAplication iIUsersAplication;
        private IConfiguration iIConfiguration;

        public UsersController
         (
            IUsersAplication _IUsersAplication,
            IConfiguration IConfiguration
            )
        {
            this.iIUsersAplication = _IUsersAplication;
            this.iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListUsers")]
        public async Task<ActionResult> GetListUsers([FromBody] UserDto request)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            try
            {

                res = await this.iIUsersAplication.GetListUsers(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("GetListUserByParameter")]
        public async Task<ActionResult> GetListUserByParameter([FromBody] UserByParameterDto request)
        {
      
            ResultDTO<UserByParameterDto> res = new ResultDTO<UserByParameterDto>();
            try
            {

                res = await this.iIUsersAplication.GetListUserByParameter(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] UserDto request)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                //UserDto buscarUsuario = new UserDto();
                //buscarUsuario.iid_user = -1;
                //buscarUsuario.vemail = request.vemail;
                //buscarUsuario.iindex = 0; 
                //buscarUsuario.ilimit = 1000;
                //buscarUsuario.istate_record = -1;
                //var resCorreo = await this.iIUsersAplication.GetListUsers(buscarUsuario);
                ////FALTA VALIDAR POR EL DNI Y NRO TELEFONICO
                //if ((request.iid_user == 0) && resCorreo.Data.FindAll(x => x.vemail == request.vemail).Count == 1)
                //{

                //    res.IsSuccess = false;
                //    res.Item = null;
                //    res.Data = null;
                //    res.Message = "El Correo ya esta Registrado.";
                //}


                //else
                //{
                res = await this.iIUsersAplication.RegisterUser(request);
                //}

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> DeleteUser([FromQuery] int iid_user)
        {
            ResultDTO<UserDto> res = new ResultDTO<UserDto>();
            try
            {
                UserDto request = new UserDto();
                request.iid_user = iid_user;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIUsersAplication.DeleteUser(request);
                
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
