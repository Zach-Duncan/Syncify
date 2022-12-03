using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/to-dos")]
    public class ToDosController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ToDosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var toDos = _dataContext
                .ToDos
                .Select(toDos => new ToDoGetDto
                {
                    Id = toDos.Id,
                    CalendarId = toDos.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = toDos.CalendarId,
                        GroupId = toDos.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = toDos.Calendar.GroupId,
                            Name = toDos.Calendar.Group.Name,
                            Image = toDos.Calendar.Group.Image
                        }
                    },
                    Title = toDos.Title,
                    Description = toDos.Description,
                    StartDate = toDos.StartDate,
                    EndDate = toDos.EndDate,
                })
                .ToList();

            response.Data = toDos;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var toDoToReturn = _dataContext
                .ToDos
                .Select(toDos => new ToDoGetDto
                {
                    Id = toDos.Id,
                    CalendarId = toDos.CalendarId,
                    Calendar = new CalendarGetDto 
                    {
                        Id = toDos.CalendarId,
                        GroupId = toDos.Calendar.GroupId,
                        Group = new GroupGetDto 
                        {
                            Id = toDos.Calendar.GroupId,
                            Name = toDos.Calendar.Group.Name,
                            Image = toDos.Calendar.Group.Image
                        }
                    },
                    Title = toDos.Title,
                    Description = toDos.Description
                })
                .FirstOrDefault(toDos => toDos.Id == id);

            if (toDoToReturn == null)
            {
                response.AddError("id", "To Do not found.");
                return BadRequest(response);
            }

            response.Data = toDoToReturn;
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoCreateDto toDoCreateDto)
        {
            var response = new Response();

            if (!_dataContext.Calendars.Any(calendar => calendar.Id == toDoCreateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar Id does not exist.");
            }

            if (string.IsNullOrEmpty(toDoCreateDto.Title))
            {
                response.AddError("Title", "Title cannot be empty.");
            }

            if (string.IsNullOrEmpty(toDoCreateDto.Description))
            {
                response.AddError("Description", "Description cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var toDoToAdd = new ToDo
            {
                CalendarId = toDoCreateDto.CalendarId, 
                Title = toDoCreateDto.Title,
                Description = toDoCreateDto.Description,
                StartDate = toDoCreateDto.StartDate,
                EndDate = toDoCreateDto.EndDate,
            };

            _dataContext.ToDos.Add(toDoToAdd);
            _dataContext.SaveChanges();

            var toDo = _dataContext
                .ToDos
                .Include(toDo => toDo.Calendar)
                .ThenInclude(toDo => toDo.Group)
                .FirstOrDefault(toDo => toDo.Id == toDoToAdd.Id);

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDo.Id,
                CalendarId = toDo.CalendarId,
                Calendar = new CalendarGetDto 
                {
                    Id = toDo.CalendarId,
                    GroupId = toDo.Calendar.GroupId,
                    Group = new GroupGetDto 
                    {
                        Id = toDo.Calendar.GroupId,
                        Name = toDo.Calendar.Group.Name,
                        Image = toDo.Calendar.Group.Image
                    }
                },
                Title = toDo.Title,
                Description = toDo.Description,
                StartDate = toDo.StartDate,
                EndDate = toDo.EndDate,
            };

            //returns 201 Code, which means created
            response.Data = toDoToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] ToDoUpdateDto toDoUpdateDto)
        {
            var response = new Response();

            var toDoToUpdate = _dataContext
                .ToDos
                .FirstOrDefault(toDo => toDo.Id == id);

            if (toDoToUpdate == null)
            {
                response.AddError("id", "Task not found.");                
            }

            if (!_dataContext.Calendars.Any(calendar => calendar.Id == toDoUpdateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar Id does not exist.");
            }

            if (string.IsNullOrEmpty(toDoUpdateDto.Title))
            {
                response.AddError("Title", "Title cannot be empty.");
            }

            if (string.IsNullOrEmpty(toDoUpdateDto.Description))
            {
                response.AddError("Description", "Description cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            toDoToUpdate.CalendarId = toDoUpdateDto.CalendarId;
            toDoToUpdate.Title = toDoUpdateDto.Title;
            toDoToUpdate.Description = toDoUpdateDto.Description;
            toDoToUpdate.StartDate = toDoUpdateDto.StartDate;
            toDoToUpdate.EndDate = toDoUpdateDto.EndDate;

            _dataContext.SaveChanges();

            var toDo = _dataContext
                .ToDos
                .Include(toDo => toDo.Calendar)
                .ThenInclude(toDo => toDo.Group)
                .FirstOrDefault(toDo => toDo.Id == toDoToUpdate.Id);

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDo.Id,
                CalendarId = toDo.Calendar.Id,
                Calendar = new CalendarGetDto 
                {
                    Id = toDo.CalendarId,
                    GroupId = toDo.Calendar.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = toDo.Calendar.GroupId,
                        Name = toDo.Calendar.Group.Name,
                        Image = toDo.Calendar.Group.Image
                    }
                },
                Title = toDo.Title,
                Description = toDo.Description,
                StartDate = toDo.StartDate,
                EndDate = toDo.EndDate,
            };

            response.Data = toDoToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var toDoToDelete = _dataContext
                .ToDos
                .FirstOrDefault(toDo => toDo.Id == id);
            if (toDoToDelete == null)
            {
                response.AddError("id", "Task not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(toDoToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }

    }
}
    
