using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IService.Masters
{
    public interface ISheduleService
    {
        public Task<ResultDTO<SheduleDto>> GetListShedule(SheduleDto request);
        public Task<ResultDTO<SheduleDto>> RegisterShedule(SheduleDto request);
        public Task<ResultDTO<SheduleDto>> DeleteShedule(SheduleDto request);
        public Task<ResultDTO<SheduleDto>> DeleteSheduleAll(SheduleDto request);


    }
}
