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
        private readonly DataContext _dataContext;
        public EventsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var events = _dataContext
                .Events
                .Select(events => new EventGetDto
                {
                    Id = events.Id,
                    CalendarId = events.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = events.CalendarId,
                        GroupId = events.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = events.Calendar.GroupId,
                            Name = events.Calendar.Group.Name,
                            Image = events.Calendar.Group.Image,
                        }
                    },
                    Name = events.Name,
                    EventDetails = events.EventDetails,
                    StartDate = events.StartDate,
                    EndDate = events.EndDate,
                })
                .ToList();

            response.Data = events;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var eventToReturn = _dataContext
                .Events
                .Select(events => new EventGetDto
                {
                    Id = events.Id,
                    CalendarId = events.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = events.CalendarId,
                        GroupId = events.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = events.Calendar.GroupId,
                            Name = events.Calendar.Group.Name,
                            Image = events.Calendar.Group.Image,
                        }
                    },
                    Name = events.Name,
                    EventDetails = events.EventDetails,
                    StartDate = events.StartDate,
                    EndDate = events.EndDate,
                })
                .FirstOrDefault(events => events.Id == id);

            if (eventToReturn == null)
            {
                response.AddError("id", "Event not found.");
                return BadRequest(response);
            }

            response.Data = eventToReturn;
            return Ok(response);
        }


        [HttpPost]
        public IActionResult Create([FromBody] EventCreateDto eventCreateDto)
        {
            var response = new Response();

            if (!_dataContext.Calendars.Any(calendars => calendars.Id == eventCreateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar Id does not exist.");
            }

            if (eventCreateDto.Name == null || eventCreateDto.Name == "")
            {
                response.AddError("Name", "Event name cannot be empty.");
            }

            if (eventCreateDto.EventDetails == null || eventCreateDto.EventDetails == "")
            {
                response.AddError("EventDetails", "Event details cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var eventToCreate = new Event
            {
                CalendarId = eventCreateDto.CalendarId,
                Name = eventCreateDto.Name,
                EventDetails = eventCreateDto.EventDetails,
                StartDate = eventCreateDto.StartDate,
                EndDate = eventCreateDto.EndDate,
            };

            _dataContext.Events.Add(eventToCreate);
            _dataContext.SaveChanges();

            var events = _dataContext
                .Events
                .Include(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .FirstOrDefault(x => x.Id == eventToCreate.Id);

            var eventToReturn = new EventGetDto
            {
                Id = events.Id,
                CalendarId = events.CalendarId,
                Calendar = new CalendarGetDto
                {
                    Id = events.CalendarId,
                    GroupId = events.Calendar.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = events.Calendar.GroupId,
                        Name = events.Calendar.Group.Name,
                        Image = events.Calendar.Group.Image,
                    }
                },
                Name = events.Name,
                EventDetails = events.EventDetails,
                StartDate = events.StartDate,
                EndDate = events.EndDate,
            };

            response.Data = eventToReturn;
            return Created("", response);
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
            }

            if (!_dataContext.Calendars.Any(calendars => calendars.Id == eventUpdateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar Id does not exist.");
            }

            if (eventUpdateDto.Name == null || eventUpdateDto.Name == "")
            {
                response.AddError("Name", "Event name cannot be empty.");
            }

            if (eventUpdateDto.EventDetails == null || eventUpdateDto.EventDetails == "")
            {
                response.AddError("EventDetails", "Event details cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }


            eventToUpdate.CalendarId = eventUpdateDto.CalendarId;
            eventToUpdate.Name = eventUpdateDto.Name;
            eventToUpdate.EventDetails = eventUpdateDto.EventDetails;
            eventToUpdate.StartDate = eventUpdateDto.StartDate;
            eventToUpdate.EndDate = eventUpdateDto.EndDate;

            _dataContext.SaveChanges();

            var events = _dataContext
                .Events
                .Include(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .FirstOrDefault(x => x.Id == eventToUpdate.Id);

            var eventToReturn = new EventGetDto
            {
                Id = events.Id,
                CalendarId = events.CalendarId,
                Calendar = new CalendarGetDto
                {
                    Id = events.CalendarId,
                    GroupId = events.Calendar.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = events.Calendar.GroupId,
                        Name = events.Calendar.Group.Name,
                        Image = events.Calendar.Group.Image,
                    }
                },
                Name = events.Name,
                EventDetails = events.EventDetails,
                StartDate = events.StartDate,
                EndDate = events.EndDate,
            };

            response.Data = eventToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var eventToDelete = _dataContext
                .Events
                .FirstOrDefault(x => x.Id == id);
            if (eventToDelete == null)
            {
                response.AddError("id", "Event not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(eventToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}

