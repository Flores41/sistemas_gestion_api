using Abstraction.IAplication.Administration;
using Abstraction.IAplication.Masters;
using Aplication.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Model;
using Model.Util;
using Models.Administration;
using Models.Masters;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiSistemaVentas.Controllers.Masters
{
    [Route("Masters/Comunity/")]
    [ApiController]
    [Authorize]

    public class ComunityController : Controller
    {
        private IComunityAplication iIComunityAplication;
        private IModuleOptionAplication iIModuleOptionAplication;

        private IConfiguration iIConfiguration;

        public ComunityController
         (
            IComunityAplication _IComunityAplication,
            IModuleOptionAplication _IModuleOptionAplication,
            IConfiguration IConfiguration)
        {
            this.iIComunityAplication = _IComunityAplication;
            this.iIModuleOptionAplication = _IModuleOptionAplication;

            this.iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListComunity")]
        public async Task<ActionResult> GetListComunity([FromBody] ComunityDto request)
        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            try
            {
                res = await this.iIComunityAplication.GetListComunity(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpPost]
        [Route("RegisterComunity")]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]

        public async Task<ActionResult> RegisterComunity(IFormCollection data, IFormFile file)
        //public async Task<ActionResult> RegisterComunity([FromBody] ComunityDto request)

        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            try
            {


                ComunityDto request = new ComunityDto();
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


                var dataJson = data["data"];
                request = JsonConvert.DeserializeObject<ComunityDto>(dataJson);

                string newNameFile = request.vname_comunity + Path.GetExtension(file.FileName);

                var rutaLocal = Util.saveFileToDisk(file, newNameFile, iIConfiguration["UploadFileTemp"]);
                string urlFirebase = iIConfiguration.GetConnectionString("CS_Firebase");

                request.vfirebase_url_comunity = await Util.SubirArchivoFirebase(rutaLocal, newNameFile, urlFirebase, FolderFirebase.strFolderComunity);
                                
                Util.deleteFileToDisk(file, newNameFile, iIConfiguration["UploadFileTemp"]);


                res = await this.iIComunityAplication.RegisterComunity(request);


                if (res.IsSuccess && request.iid_comunity == 0)
                {
                    OptionDto option = new OptionDto
                    {
                        iid_option = 0,
                        iid_module = 4,//Comunidades
                        vname_option = request.vname_comunity,
                        vdescription_option = request.vdescription_comunity,
                        vurl_option = "/Comunity/"+ request.vname_comunity,
                        vicon_option = "building",
                        iorder_option = 0,
                        bvisible_option = true,
                        iid_comunity = res.Code,
                        istate_record = 1,
                        iid_user_token = request.iid_user_token
                    };

                    ResultDTO<OptionDto> resOptionComunity = await this.iIModuleOptionAplication.RegisterOption(option);//Comunidad

                    option.iid_module = 5;//HORARIOS
                    option.vname_option = "Horario " + request.vname_comunity;
                    option.vdescription_option = "Horario " + request.vdescription_comunity;
                    option.vurl_option = "/Shedules/" + request.vname_comunity;
                    option.vicon_option = "calendar";

                    ResultDTO<OptionDto> resOptionShedule = await this.iIModuleOptionAplication.RegisterOption(option);//HORARIO
                }
                else
                {
                    OptionDto comunity = new OptionDto
                    {
                        iid_option = -1,
                        iid_module = -1,
                        vname_option = "",
                        vdescription_option = "",
                        iid_comunity = request.iid_comunity,
                        istate_record = 1,
                        iid_user_token = request.iid_user_token,
                        iindex=0,
                        ilimit= 100
                    };

                    var _resOptionComunity = await this.iIModuleOptionAplication.GetListOption(comunity);//Comunidad

                    if (_resOptionComunity.IsSuccess)
                    {
                        OptionDto option = new OptionDto
                        {
                            iid_option = _resOptionComunity.Data.Find(p => p.iid_module == 4).iid_option,
                            iid_module = 4,//Comunidades
                            vname_option = request.vname_comunity,
                            vdescription_option = request.vdescription_comunity,
                            vurl_option = "/Comunity/" + request.vname_comunity,
                            vicon_option = "building",
                            istate_record = 1,
                            iid_user_token = request.iid_user_token
                        };

                        ResultDTO<OptionDto> resOptionComunity = await this.iIModuleOptionAplication.RegisterOption(option);//Comunidad

                        option.iid_option = _resOptionComunity.Data.Find(p => p.iid_module == 5).iid_option;//HORARIOS
                        option.iid_module = 5;//HORARIOS
                        option.vname_option = "Horario " + request.vname_comunity;
                        option.vdescription_option = "Horario " + request.vdescription_comunity;
                        option.vurl_option = "/Shedules/" + request.vname_comunity;
                        option.vicon_option = "calendar";

                        ResultDTO<OptionDto> resOptionShedule = await this.iIModuleOptionAplication.RegisterOption(option);//HORARIO
                    }
                }

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteComunity")]
        public async Task<ActionResult> DeleteComunity([FromQuery] int iid_comunity)
        {
            ResultDTO<ComunityDto> res = new ResultDTO<ComunityDto>();
            try
            {
                ComunityDto request = new ComunityDto();
                request.iid_comunity = iid_comunity;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await this.iIComunityAplication.DeleteComunity(request);

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
