using Model;
using Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Masters
{
    public interface IProvidersRepository
    {
        public Task<ResultDTO<ProvidersDto>> GetListProviders(ProvidersDto request);
        public Task<ResultDTO<ProvidersDto>> RegisterProvider(RegisterProvidersDto request);
        public Task<ResultDTO<ProvidersDto>> DeleteProvider(ProvidersDto request);
    }
}
