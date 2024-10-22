using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IService.Masters
{
    public interface IProvidersService
    {
        public Task<ResultDTO<ProvidersDto>> GetListProviders(ProvidersDto request);
        public Task<ResultDTO<ProvidersDto>> RegisterProvider(RegisterProvidersDto request);
        public Task<ResultDTO<ProvidersDto>> DeleteProvider(ProvidersDto request);
    }
}
