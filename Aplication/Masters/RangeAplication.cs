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
    public class RangeAplication : IRangeAplication
    {
        private readonly IRangeService _IRangeService;
        public RangeAplication(IRangeService IRangeService)
        {
            this._IRangeService = IRangeService;
        }


        public Task<ResultDTO<RangeDto>> GetListRange(RangeDto request)
        {
            return this._IRangeService.GetListRange(request);
        }
        public Task<ResultDTO<RangeDto>> RegisterRange(RangeDto request)
        {
            return this._IRangeService.RegisterRange(request);
        }
        public Task<ResultDTO<RangeDto>> DeleteRange(RangeDto request)
        {
            return this._IRangeService.DeleteRange(request);
        }

    }
}
