using Abstraction.IRepository.Administration;
using Abstraction.IRepository.Masters;
using Abstraction.IService.Administration;
using Abstraction.IService.Masters;
using Model;
using Models.Administration;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Masters
{
    public class ComunityService : IComunityService
    {
        public readonly IComunityRepository _IComunityRepository;

        public ComunityService(IComunityRepository IComunityRepository)
        {
            this._IComunityRepository = IComunityRepository;
        }


        public Task<ResultDTO<ComunityDto>> GetListComunity(ComunityDto request)
        {
            return this._IComunityRepository.GetListComunity(request);
        }

        public Task<ResultDTO<ComunityDto>> RegisterComunity(ComunityDto request)
        {
            return this._IComunityRepository.RegisterComunity(request);
        }

        public Task<ResultDTO<ComunityDto>> DeleteComunity(ComunityDto request)
        {
            return this._IComunityRepository.DeleteComunity(request);
        }
    }
}
