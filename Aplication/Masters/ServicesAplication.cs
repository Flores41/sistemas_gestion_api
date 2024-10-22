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
    public class ServicesAplication : IServicesAplication
    {
        private readonly IServicesService _IServicesService;
        public ServicesAplication(IServicesService IServicesService)
        {
            _IServicesService = IServicesService;
        }

        public Task<ResultDTO<ServicesDto>> GetListServices(ServicesDto request)
        {
            return _IServicesService.GetListServices(request);
        }

        public Task<ResultDTO<ServicesDto>> RegisterService(ServicesRegisterDto request)
        {
            return _IServicesService.RegisterService(request);
        }

        public Task<ResultDTO<ServicesDto>> DeleteService(ServicesDto request)
        {
            return _IServicesService.DeleteService(request);
        }
    }

}
