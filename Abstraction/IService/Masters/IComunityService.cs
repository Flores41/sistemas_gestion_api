using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IService.Masters
{
    public interface IComunityService
    {
        public Task<ResultDTO<ComunityDto>> GetListComunity(ComunityDto request);
        public Task<ResultDTO<ComunityDto>> RegisterComunity(ComunityDto request);
        public Task<ResultDTO<ComunityDto>> DeleteComunity(ComunityDto request);
    }
}
