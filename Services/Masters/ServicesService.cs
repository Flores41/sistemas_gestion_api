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
    public class ServicesService : IServicesService
    {
        public readonly IServicesRepository _IServicesRepository;

        public ServicesService(IServicesRepository IServicesRepository)
        {
            _IServicesRepository = IServicesRepository;
        }


        public Task<ResultDTO<ServicesDto>> GetListServices(ServicesDto request)
        {
            return _IServicesRepository.GetListServices(request);
        }

        public Task<ResultDTO<ServicesDto>> RegisterService(ServicesRegisterDto request)
        {
            return _IServicesRepository.RegisterService(request);
        }

        public Task<ResultDTO<ServicesDto>> DeleteService(ServicesDto request)
        {
            return _IServicesRepository.DeleteService(request);
        }
    }
}
