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
    public class EventsAplication : IEventsAplication
    {
        private readonly IEventsService _IEventsService;
        public EventsAplication(IEventsService IEventsService)
        {
            _IEventsService = IEventsService;
        }

        public Task<ResultDTO<EventsDto>> GetListEvents(EventsDto request)
        {
            return _IEventsService.GetListEvents(request);
        }

        public Task<ResultDTO<SaveEventsDTO>> SaveEvents(SaveEventsDTO request)
        {
            return _IEventsService.SaveEvents(request);
        }

        public Task<ResultDTO<EventsDto>> DeleteEvents(EventsDto request)
        {
            return _IEventsService.DeleteEvents(request);
        }
    }
}
