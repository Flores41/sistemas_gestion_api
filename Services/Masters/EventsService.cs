using Abstraction.IRepository.Administration;
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

    public class EventsService : IEventsService
    {
        public readonly IEventsRepository _IEventsRepository;

        public EventsService(IEventsRepository IEventsRepository)
        {
            _IEventsRepository = IEventsRepository;
        }


        public Task<ResultDTO<EventsDto>> GetListEvents(EventsDto request)
        {
            return _IEventsRepository.GetListEvents(request);
        }

        public Task<ResultDTO<SaveEventsDTO>> SaveEvents(SaveEventsDTO request)
        {
            return _IEventsRepository.SaveEvents(request);
        }

        public Task<ResultDTO<EventsDto>> DeleteEvents(EventsDto request)
        {
            return _IEventsRepository.DeleteEvents(request);
        }
    }
}
