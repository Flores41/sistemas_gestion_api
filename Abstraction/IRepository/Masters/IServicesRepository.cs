using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Masters
{
    public interface IServicesRepository
    {
        public Task<ResultDTO<ServicesDto>> GetListServices(ServicesDto request);
        public Task<ResultDTO<ServicesDto>> RegisterService(ServicesRegisterDto request);
        public Task<ResultDTO<ServicesDto>> DeleteService(ServicesDto request);
    }
}
