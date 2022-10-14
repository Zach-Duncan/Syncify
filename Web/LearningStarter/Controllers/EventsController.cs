using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private DataContext _dataContext;

        public object eventToReturn { get; private set; }

        public EventsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var eventsToReturn = _dataContext
                .Events
                .Select(x => new EventGetDto
                {
                    Id = x.Id,
                    CalendarId = x.CalendarId,
                    Name = x.Name,
                    EventDetails = x.EventDetails,
                    CreatedDate = x.CreatedDate,
                })
                .ToList();


            response.Data = eventsToReturn;

            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            if (id <= 0)
            {
                response.AddError("id", "Cannot be less than or equal to zero");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var eventFromDatabase = _dataContext
                .Events
                .FirstOrDefault(events => events.Id == id);

            if (eventFromDatabase == null)
            {
                response.AddError("id", "No Event Found");
                return NotFound(response);
            }

            var eventToReturn = new EventGetDto
            {
                Id = eventFromDatabase.Id,
                Name = eventFromDatabase.Name,
                EventDetails = eventFromDatabase.EventDetails,
                CreatedDate = eventFromDatabase.CreatedDate,
            };

            response.Data = eventToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventCreateDto eventCreateDto)
        {
            var response = new Response();

            if (eventCreateDto == null)
            {
                response.AddError("", "Critical error.");
                return BadRequest(response);
            }



            if (eventCreateDto.Name == null) 
             {
                response.AddError("name", "Event name cannot be empty.");
                return BadRequest(response);
            }

            var eventAlreadyExistsInDatabase = _dataContext.Events.Any(x => x.Name == eventCreateDto.Name);

            if (eventAlreadyExistsInDatabase)
            {
                response.AddError("name", "Event already exists in database");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var eventToCreate = new Event
            {
                Name = eventCreateDto.Name,
                CalendarId = eventCreateDto.CalendarId,
                EventDetails = eventCreateDto.EventDetails,
                CreatedDate = eventCreateDto.CreatedDate,
            };

            _dataContext.Events.Add(eventToCreate);
            _dataContext.SaveChanges();

            return Created("api/events/" + eventToCreate.Id,
                eventToReturn);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] EventUpdateDto eventUpdateDto)
        {
            var response = new Response();

            var eventToUpdate = _dataContext
                .Events
                .FirstOrDefault(events => events.Id == id);

            if (eventToUpdate == null)
            {
                response.AddError("id", "Task not found.");
                return BadRequest(response);
            }

            eventToUpdate.EventDetails = eventUpdateDto.EventDetails;
            _dataContext.SaveChanges();

            var eventsToReturn = new EventGetDto
            {
                Id = eventToUpdate.Id,
                EventDetails = eventToUpdate.EventDetails,
                Name = eventToUpdate.Name,
                CreatedDate = eventToUpdate.CreatedDate
            };

            response.Data = eventToReturn;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var events = _dataContext.Events.FirstOrDefault(x => x.Id == id);

            if (events == null)
            {
                response.AddError("id", "There was a problem deleting the event.");
                return NotFound(response);
            }

            _dataContext.Events.Remove(events);
            _dataContext.SaveChanges();

            return Ok(response);
        }

        

    }
}
