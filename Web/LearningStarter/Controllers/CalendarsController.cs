using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/calendars")]

    public class CalendarsController : Controller
    {
        private readonly DataContext _dataContext;
        public CalendarsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var response = new Response();

            var calendars = _dataContext.Calendars.Select(calendar => new CalendarGetDto
            {
                Id = calendar.Id,
                Group = calendar.Group,
                GroupId = calendar.GroupId,
            })
                .ToList();

            response.Data = calendars;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var calendarToReturn = _dataContext
                .Calendars
                .Select(calendar => new CalendarGetDto
                {
                    Id = calendar.Id,
                    Group = calendar.Group,
                    GroupId = calendar.GroupId,
                })
                .FirstOrDefault(calendar => calendar.Id == id);
            if (calendarToReturn == null)
            {
                response.AddError("id", "Calendar not found.");
                return BadRequest(response);
            }

            response.Data = calendarToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] CalendarCreateDto calendarCreateDto)
        {
            var response = new Response();

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var calendarToAdd = new Calendar
            {
                Group = calendarCreateDto.Group,
            };

            _dataContext.Calendars.Add(calendarToAdd);
            _dataContext.SaveChanges();

            var calendarToReturn = new CalendarGetDto
            {
                GroupId = calendarToAdd.GroupId,
                Group = calendarToAdd.Group,
            };

            response.Data = calendarToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] CalendarUpdateDto calendarUpdateDto)
        {
            var response = new Response();

            var calendarToUpdate = _dataContext
                .Calendars
                .FirstOrDefault(calendar => calendar.Id == id);
            
            if (calendarToUpdate == null)
            {
                response.AddError("id", "Calendar not found.");
                return BadRequest(response);
            }
            calendarToUpdate.Group = calendarUpdateDto.Group;
            _dataContext.SaveChanges();

            var calendarToReturn = new CalendarGetDto
            {
                GroupId = calendarToUpdate.GroupId,
                Group = calendarToUpdate.Group
            };

            response.Data = calendarToReturn;
            return Ok(response);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var calendarToDelete = _dataContext
                .Calendars
                .FirstOrDefault(calendar => calendar.Id == id);
            
            if (calendarToDelete == null)
            {
                response.AddError("id", "Calendar not found.");
                return BadRequest(response);
            }
            
            _dataContext.Remove(calendarToDelete);
            _dataContext.SaveChanges();
            response.Data = true;
            return Ok(response);
        }

    }
    }
    

