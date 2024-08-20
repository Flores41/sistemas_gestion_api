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
    public class SheduleAplication : ISheduleAplication
    {
        private readonly ISheduleService _ISheduleService;
        public SheduleAplication(ISheduleService ISheduleService)
        {
            this._ISheduleService = ISheduleService;
        }



        public Task<ResultDTO<SheduleDto>> GetListShedule(SheduleDto request)
        {
            return this._ISheduleService.GetListShedule(request);
        }

        public Task<ResultDTO<SheduleDto>> RegisterShedule(SheduleDto request)
        {
            return this._ISheduleService.RegisterShedule(request);
        }

        public Task<ResultDTO<SheduleDto>> DeleteShedule(SheduleDto request)
        {
            return this._ISheduleService.DeleteShedule(request);
        }
        public Task<ResultDTO<SheduleDto>> DeleteSheduleAll(SheduleDto request)
        {
            return this._ISheduleService.DeleteSheduleAll(request);
        }
    }
}
