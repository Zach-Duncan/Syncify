using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/toDos")]
    public class ToDosController : ControllerBase
    {
        private DataContext _dataContext;

        public object toDoToReturn { get; set; }

        public ToDosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var toDosToReturn = _dataContext
                .ToDos
                .Select(toDo => new ToDoGetDto
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
                    Date = toDo.Date,
                })
                .ToList();
            response.Data = toDosToReturn;

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

            var toDosFromDatabase = _dataContext
                .ToDos
                .FirstOrDefault(toDo => toDo.Id == id);

            if (toDosFromDatabase == null)
            {
                response.AddError("id", "No Task Found");
                return NotFound(response);
            }

            var toDo = _dataContext
                .ToDos
                .Include(toDo => toDo.Calendar)
                .ThenInclude(toDo => toDo.Group)
                .FirstOrDefault(toDo => toDo.Id == toDosFromDatabase.Id);

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDosFromDatabase.Id,
                CalendarId = toDosFromDatabase.CalendarId,
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
                Title = toDosFromDatabase.Title,
                Description = toDosFromDatabase.Description
            };

            response.Data = toDoToReturn;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoCreateDto toDoCreateDto)
        {
            var response = new Response();

            if (toDoCreateDto == null)
            {
                response.AddError("", "Critical error.");
                return BadRequest(response);
            }
            if (!_dataContext.Calendars.Any(calendar => calendar.Id == toDoCreateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar does not exist.");
            }
            if (toDoCreateDto.Title == null || toDoCreateDto.Title == "")
            {
                response.AddError("Title", "Task title cannot be empty");
                return BadRequest(response);
            }
            if (toDoCreateDto.Description == null || toDoCreateDto.Description == "") 
            {
                response.AddError("Description", "Task Description cannot be empty");
                return BadRequest(response);
            }
            var toDoAlreadyExistsInDatabase = _dataContext.ToDos.Any(toDo => toDo.Title == toDoCreateDto.Title);

            if (toDoAlreadyExistsInDatabase)
            {
                response.AddError("title", "Already exists in ToDo.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }


            var toDoToCreate = new ToDo
            {
                CalendarId = toDoCreateDto.CalendarId, 
                Title = toDoCreateDto.Title,
                Description = toDoCreateDto.Description,
                Date = toDoCreateDto.Date,
                // User = shoppingListCreateDto.User,
            };

            _dataContext.ToDos.Add(toDoToCreate);
            _dataContext.SaveChanges();

            var toDo = _dataContext
                .ToDos
                .Include(toDo => toDo.Calendar)
                .ThenInclude(toDo => toDo.Group)
                .FirstOrDefault(toDo => toDo.Id == toDoToCreate.Id);

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
                Title = toDoToCreate.Title,
                Description = toDoCreateDto.Description,
                Date = toDoCreateDto.Date
            };

            //returns 201 Code, which means created
            return Created("api/to-dos/" + toDoToCreate.Id,
                toDoToReturn);
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
                return BadRequest(response);
            }

            toDoToUpdate.Title = toDoUpdateDto.Title;
            _dataContext.SaveChanges();

            var toDo = _dataContext
                .ToDos
                .Include(toDo => toDo.Calendar)
                .ThenInclude(toDo => toDo.Group)
                .FirstOrDefault(toDo => toDo.Id == toDoToUpdate.Id);

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDoToUpdate.Id,
                CalendarId = toDoToUpdate.Calendar.Id,
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
                Title = toDoToUpdate.Title,
                Description = toDoToUpdate.Description,
                Date = toDoToUpdate.Date
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
    
