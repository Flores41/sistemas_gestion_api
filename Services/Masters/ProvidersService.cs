using Abstraction.IRepository.Administration;
using Abstraction.IRepository.Masters;
using Abstraction.IService.Masters;
using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Masters
{
    public class ProvidersService : IProvidersService
    {
        public readonly IProvidersRepository _IProvidersRepository;

        public ProvidersService(IProvidersRepository IProvidersRepository)
        {
            _IProvidersRepository = IProvidersRepository;
        }


        public Task<ResultDTO<ProvidersDto>> GetListProviders(ProvidersDto request)
        {
            return _IProvidersRepository.GetListProviders(request);
        }

        public Task<ResultDTO<ProvidersDto>> RegisterProvider(RegisterProvidersDto request)
        {
            return _IProvidersRepository.RegisterProvider(request);
        }

        public Task<ResultDTO<ProvidersDto>> DeleteProvider(ProvidersDto request)
        {
            return _IProvidersRepository.DeleteProvider(request);
        }
    }
}
