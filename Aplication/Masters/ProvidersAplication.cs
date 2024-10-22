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
    public class ProvidersAplication : IProvidersAplication
    {
        private readonly IProvidersService _IProvidersService;
        public ProvidersAplication(IProvidersService IProvidersService)
        {
            _IProvidersService = IProvidersService;
        }

        public Task<ResultDTO<ProvidersDto>> GetListProviders(ProvidersDto request)
        {
            return _IProvidersService.GetListProviders(request);
        }

        public Task<ResultDTO<ProvidersDto>> RegisterProvider(RegisterProvidersDto request)
        {
            return _IProvidersService.RegisterProvider(request);
        }

        public Task<ResultDTO<ProvidersDto>> DeleteProvider(ProvidersDto request)
        {
            return _IProvidersService.DeleteProvider(request);
        }
    }
}
