using Abstraction.IAplication.Masters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Models.Masters;
using System.Security.Claims;

namespace ApiGestionEventos.Controllers.Masters
{


    [Route("Masters/Events/")]
    [ApiController]
    [Authorize]

    public class EventsController : Controller
    {
        private IEventsAplication iIEventsAplication;
        private IConfiguration iIConfiguration;

        public EventsController
         (
            IEventsAplication _IEventsAplication,
            IConfiguration IConfiguration
            )
        {
            iIEventsAplication = _IEventsAplication;
            iIConfiguration = IConfiguration;
        }


        [HttpPost]
        [Route("GetListEvents")]
        public async Task<ActionResult> GetListEvents([FromBody] EventsDto request)
        {
            ResultDTO<EventsDto> res = new ResultDTO<EventsDto>();
            try
            {

                res = await iIEventsAplication.GetListEvents(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }


        [HttpPost]
        [Route("SaveEvent")]
        public async Task<ActionResult> SaveEvent([FromBody] SaveEventsDTO request)
        {
            ResultDTO<SaveEventsDTO> res = new ResultDTO<SaveEventsDTO>();
            try
            {
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIEventsAplication.SaveEvents(request);


                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

        [HttpDelete]
        [Route("DeleteEvent")]
        public async Task<ActionResult> DeleteEvent([FromQuery] int iid_event)
        {
            ResultDTO<EventsDto> res = new ResultDTO<EventsDto>();
            try
            {
                EventsDto request = new EventsDto();
                request.iid_event = iid_event;
                request.iid_user_token = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                res = await iIEventsAplication.DeleteEvents(request);

                return Ok(res);
            }
            catch (Exception e)
            {

                res.InnerException = e.Message.ToString();

                return BadRequest(res);
            }
        }

    }
}
