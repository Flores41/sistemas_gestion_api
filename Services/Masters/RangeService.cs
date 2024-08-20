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
    public class RangeService : IRangeService
    {
        public readonly IRangeRepository _IRangeRepository;

        public RangeService(IRangeRepository IRangeRepository)
        {
            this._IRangeRepository = IRangeRepository;
        }



        public Task<ResultDTO<RangeDto>> GetListRange(RangeDto request)
        {
            return this._IRangeRepository.GetListRange(request);
        }
        public Task<ResultDTO<RangeDto>> RegisterRange(RangeDto request)
        {
            return this._IRangeRepository.RegisterRange(request);
        }
        public Task<ResultDTO<RangeDto>> DeleteRange(RangeDto request)
        {
            return this._IRangeRepository.DeleteRange(request);
        }
    }
}
