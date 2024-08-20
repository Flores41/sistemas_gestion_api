using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IService.Masters
{
    public interface IRangeService
    {
        public Task<ResultDTO<RangeDto>> GetListRange(RangeDto request);
        public Task<ResultDTO<RangeDto>> RegisterRange(RangeDto request);
        public Task<ResultDTO<RangeDto>> DeleteRange(RangeDto request);
    }
}
