using Model.Usuario;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Util;

namespace Abstraction.IAplication.Util
{
    public interface IUtilAplication
    {
        //public Task<ResultDTO<bool>> envioMailPlantilla(int idplantilla, List<UsuarioDTO> usuario, List<UnidadNegocioDTO> unidad, int periodo, string observacion = "", string nombrePoyecto = "");
        //public Task<ResultDTO<bool>> envioMailPlantillaForecast(PlantillaCorreoDTO plantilla, List<UsuarioDTO> usuario, List<UnidadNegocioDTO> unidad, ForcastDTO proceso);
        //public Task<ResultDTO<bool>> envioCorreoPlantilla(int idplantilla, List<UsuarioDTO> usuario, List<UnidadNegocioDTO> unidad, string periodo, string observacion = "", string nombrePoyecto = "");
        //public Task<ResultDTO<bool>> envioCorreoPlantillaForecast(PlantillaCorreoDTO plantilla, List<UsuarioDTO> usuario, List<UnidadNegocioDTO> unidad, ForcastDTO proceso);
        //public Task<ResultDTO<bool>> envioMailPlantillaRClave(int idplantilla, UsuarioDTO usuario, string token);
        public Task<ResultDTO<bool>> envioMail(EmailDTO email);
        //public MemoryStream CreateExcel<T>(List<T> model, String nombreReporte, string param, string modelplantilla, bool sinTitulo);
    }
}
