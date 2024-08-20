using Abstraction;
using Abstraction.IAplication.Administration;
using Abstraction.IApplication.Auth;
using Aplication.Administration;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Administration;
using Models.Usuario;
using Services;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersAplication iIUsersAplication;
        private IConfiguration iIConfiguration;
        private IProfileAplication iIProfileAplication;
        private readonly ITokenHandlerService iITokenHandlerService;
        private readonly IAuthenticationApplication iIAuthenticationApplication;

        public AuthController
         (
            IUsersAplication _IUsersAplication,
            IProfileAplication _IProfileAplication,
            ITokenHandlerService ITokenHandlerService,
            IAuthenticationApplication IAuthenticationApplication,

            IConfiguration IConfiguration
            )
        {
            this.iIUsersAplication = _IUsersAplication;
            this.iIProfileAplication = _IProfileAplication;
            this.iITokenHandlerService = ITokenHandlerService;
            this.iIAuthenticationApplication = IAuthenticationApplication;

            this.iIConfiguration = IConfiguration;
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {

            ResultDTO<AuthenticationResponse> resLogin = new ResultDTO<AuthenticationResponse>();

           AuthenticationResponse itemLogin = new AuthenticationResponse();

            ResultDTO<UserByParameterDto> res = new ResultDTO<UserByParameterDto>();


            UserByParameterDto resUser = new UserByParameterDto();
            resUser.vnumber_document = request.vnumber_document;
            resUser.vpassword_user = request.vpassword_user;
            string contrasena = String.Empty;

            try
            {
                res = await this.iIUsersAplication.GetListUserByParameter(resUser);
                var _user = res.Item;

                if (!res.IsSuccess && res.Item == null)
                {
                    resLogin.IsSuccess = false;
                    resLogin.Message = "Usuario No Registrado";
                    resLogin.InnerException = res.InnerException;

                    resLogin.MessageExeption = res.Message + " " + res.InnerException + " " + res.MessageExeption;
                    resLogin.Token = "";

                    return Ok(resLogin);
                }
                else if (res.IsSuccess &&
                            res.Item != null &&
                            request.vpassword_user != res.Item.vpassword_user/* && 
                            res.item.iid_indica_bloqueo == 0*/
                            )
                {
                    resLogin.IsSuccess = false;
                    resLogin.InnerException = "Contraseña Incorrecta.Ingresela Nuevamente";
                    resLogin.Message = "Credenciales Invalidas.";
                    resLogin.Token = "";

                    /** registra intento de logeo **/
                    //resUser.iid_usuario = res.item.iid_usuario;
                    //await this.iIUsuarioAplication.RegisterUsuarioIntentoLogeo(resUser);

                    return Ok(resLogin);
                }
                //else if (res.IsSuccess && res.Item != null && res.Item.iid_indica_bloqueo == 1)
                //{

                //    DateTime fbloqueo = res.item.dfecha_bloqueo.AddHours(-5);
                //    DateTime factual = DateTime.Now;
                //    /* */
                //    DateTime myDate = DateTime.ParseExact(res.item.dfecha_bloqueo.AddHours(-5).ToString("HHmm"), "HHmm", System.Globalization.CultureInfo.InvariantCulture);
                //    DateTime myDate2 = DateTime.ParseExact(DateTime.Now.ToString("HHmm"), "HHmm", System.Globalization.CultureInfo.InvariantCulture);
                //    var minutes = (myDate2 - myDate).TotalMinutes;

                //    //se desbloquea luego de 5 min
                //    if (fbloqueo.Year == factual.Year && fbloqueo.Month == factual.Month && fbloqueo.Day == factual.Day)
                //    {
                //        if (minutes >= 5)//(fbloqueo.Hour == factual.Hour &&    (factual.Minute > fbloqueo.AddMinutes(5).Minute  ) )
                //        {
                //            res.item.cantidad_intentos = 0;
                //            res.item.iid_indica_bloqueo = 0;
                //            res.item.iid_estado_registro = 1;
                //            res.item.vclave = "";
                //            await this.iIUsuarioAplication.RegisterUsuario(res.item);
                //        }
                //    }


                //    resLogin.IsSuccess = false;
                //    resLogin.Message = "Usuario, " + res.item.vcorreo_electronico + " actualmente bloqueado";
                //    resLogin.Token = "";

                //    return Ok(resLogin);
                //}

                else
                {

                    resLogin.IsSuccess = true;
                    resLogin.Message = "Usuario, Logeado ";


                    /********registra acceso****/
                    //res.Item.iid_user = res.Item.iid_user;
                    //await this.iIUsuarioAplication.RegisterUsuarioAcceso(res.item);


                    /*********info usuario ************/
                    ProfileAccessDTO requestMenu = new ProfileAccessDTO
                    {
                        iid_profile = _user.iid_profile_user,
                        iindex = 0,
                        ilimit = 100000
                    };

                    var _menu =  await this.iIProfileAplication.GetListProfileAccess(requestMenu);

                 
                   

                    /********* token usuario ************/
                    ITokenParameters tparm = new ITokenParameters();
                    tparm.UserName = _user.vchannel_twitch_user;
                    tparm.PasswordHash = _user.vpassword_user;
                    tparm.Id = _user.iid_user.ToString();
                    tparm.FechaCaduca = DateTime.Now;

                    resLogin.Token = this.iITokenHandlerService.GenerateToken(tparm);

                    _user.vpassword_user = null;
                    itemLogin.menu = _menu.Data.FindAll(p => p.baccess_view == true);
                    itemLogin.userdata = _user;

                    resLogin.IsSuccess = true;
                    resLogin.Item = itemLogin;

                    return Ok(resLogin);
                }


            }
            catch (Exception e)
            {
                res.InnerException = e.Message.ToString();

                //var sorigen = "";
                //foreach (object c in this.ControllerContext.RouteData.Values.Values)
                //{
                //    sorigen += c.ToString() + " | ";
                //}
                //lg.iid_usuario_registra = 0;
                //lg.vdescripcion = e.Message.ToString();
                //lg.vcodigo_mensaje = e.Message.ToString();
                //lg.vorigen = sorigen;

                //_ = Task.Run(() =>
                //{
                //    this.iLogErrorAplication.RegisterLogError(lg);
                //});
                return BadRequest(res);
            }

        }

    }
}
