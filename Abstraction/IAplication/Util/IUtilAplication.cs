using Model.Usuario;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IAplication.Util
{
    public interface IUtilAplication
    {
        //public Task<ResultDTO<bool>> envioMailPlantillaRClave(int idplantilla, UsuarioDTO usuario, string token);
      //  public Task<ResultDTO<bool>> envioMail(EmailDTO email);
        public MemoryStream CreateExcel<T>(List<T> model, String nombreReporte, string param, string modelplantilla, bool sinTitulo);
    }
}
