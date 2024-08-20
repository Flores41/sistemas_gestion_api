using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Masters
{
    public interface IRangeRepository
    {
        public Task<ResultDTO<RangeDto>> GetListRange(RangeDto request);
        public Task<ResultDTO<RangeDto>> RegisterRange(RangeDto request);
        public Task<ResultDTO<RangeDto>> DeleteRange(RangeDto request);
    }
}
