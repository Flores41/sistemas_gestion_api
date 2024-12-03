using Model;
using Models.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.IRepository.Masters
{

    public interface IEventsRepository
    {
        public Task<ResultDTO<EventsDto>> GetListEvents(EventsDto request);
        public Task<ResultDTO<SaveEventsDTO>> SaveEvents(SaveEventsDTO request);
        public Task<ResultDTO<EventsDto>> DeleteEvents(EventsDto request);
    }
}
