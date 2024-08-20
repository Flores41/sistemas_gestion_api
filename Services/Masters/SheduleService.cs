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
    public class SheduleService : ISheduleService
    {
        public readonly ISheduleRepository _ISheduleRepository;

        public SheduleService(ISheduleRepository ISheduleRepository)
        {
            this._ISheduleRepository = ISheduleRepository;
        }


        public Task<ResultDTO<SheduleDto>> GetListShedule(SheduleDto request)
        {
            return this._ISheduleRepository.GetListShedule(request);
        }

        public Task<ResultDTO<SheduleDto>> RegisterShedule(SheduleDto request)
        {
            return this._ISheduleRepository.RegisterShedule(request);
        }

        public Task<ResultDTO<SheduleDto>> DeleteShedule(SheduleDto request)
        {
            return this._ISheduleRepository.DeleteShedule(request);
        }
        public Task<ResultDTO<SheduleDto>> DeleteSheduleAll(SheduleDto request)
        {
            return this._ISheduleRepository.DeleteSheduleAll(request);
        }
    }
}
