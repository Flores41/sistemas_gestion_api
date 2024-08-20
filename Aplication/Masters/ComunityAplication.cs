using Abstraction.IAplication.Masters;
using Abstraction.IService.Masters;
using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Masters
{
    public class ComunityAplication : IComunityAplication
    {
        private readonly IComunityService _IComunityService;
        public ComunityAplication(IComunityService IComunityService)
        {
            this._IComunityService = IComunityService;
        }


        public Task<ResultDTO<ComunityDto>> GetListComunity(ComunityDto request)
        {
            return this._IComunityService.GetListComunity(request);
        }

        public Task<ResultDTO<ComunityDto>> RegisterComunity(ComunityDto request)
        {
            return this._IComunityService.RegisterComunity(request);
        }
        public Task<ResultDTO<ComunityDto>> DeleteComunity(ComunityDto request)
        {
            return this._IComunityService.DeleteComunity(request);
        }

    }
}
