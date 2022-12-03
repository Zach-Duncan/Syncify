using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

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

            var calendars = _dataContext
                .Calendars
                .Select(calendars => new CalendarGetDto
            {
                Id = calendars.Id,
                GroupId = calendars.GroupId,
                Group = new GroupGetDto
                {
                    Id = calendars.GroupId,
                    Name = calendars.Group.Name,
                    Image = calendars.Group.Image
                }
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
                .Select(calendars => new CalendarGetDto
                {
                    Id = calendars.Id,
                    GroupId = calendars.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = calendars.GroupId,
                        Name = calendars.Group.Name,
                        Image = calendars.Group.Image
                    }
                })
                .FirstOrDefault(calendars => calendars.Id == id);

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

            if (!_dataContext.Groups.Any(group => group.Id == calendarCreateDto.GroupId))
            {
                response.AddError("GroupId", "Group does not exist.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var calendarToAdd = new Calendar
            {
                GroupId = calendarCreateDto.GroupId,
            };

            _dataContext.Calendars.Add(calendarToAdd);
            _dataContext.SaveChanges();

            var calendar = _dataContext
                .Calendars
                .Include(x => x.Group)
                .FirstOrDefault(x => x.Id == calendarToAdd.Id);

            var calendarToReturn = new CalendarGetDto
            {
                Id = calendar.Id,
                GroupId = calendar.GroupId,
                Group = new GroupGetDto
                {
                    Id = calendar.GroupId,
                    Name = calendar.Group.Name,
                    Image = calendar.Group.Image
                }
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
            }

            if (!_dataContext.Groups.Any(group => group.Id == calendarUpdateDto.GroupId))
            {
                response.AddError("GroupId", "Group Id was not found");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }


            calendarToUpdate.GroupId = calendarUpdateDto.GroupId;

            _dataContext.SaveChanges();

            var calendar = _dataContext
                .Calendars
                .Include(x => x.Group)
                .FirstOrDefault(x => x.Id == calendarToUpdate.Id);

            var calendarToReturn = new CalendarGetDto
            {
                Id = calendar.Id,
                GroupId = calendar.GroupId,
                Group = new GroupGetDto
                {
                    Id = calendar.GroupId,
                    Name = calendar.Group.Name,
                    Image = calendar.Group.Image
                }
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

        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();

            var calendar = _dataContext.Calendars
                .Select(calendar => new OptionDto(calendar.Group.Name, calendar.Id))
                .ToList();

            response.Data = calendar;

            return Ok(response);
        }

    }
}
    

