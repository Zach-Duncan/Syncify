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

        public object toDoToReturn { get; private set; }

        public ToDosController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var toDOsToReturn = _dataContext
                .ToDos
                .Select(x => new ToDoGetDto
                {
                    Id = x.Id,
                    CalendarId = x.CalendarId,
                    TaskTitle = x.TaskTitle,
                    TaskDescription = x.TaskDescription,
                    CreatedDate = x.CreatedDate,
                })
                .ToList();
            response.Data = toDoToReturn;

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
                .FirstOrDefault(x => x.Id == id);

            if (toDosFromDatabase == null)
            {
                response.AddError("id", "No Task Found");
                return NotFound(response);
            }

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDosFromDatabase.Id,
                TaskTitle = toDosFromDatabase.TaskTitle,
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
            if (toDoCreateDto.TaskTitle == null || toDoCreateDto.TaskTitle == "")
            {
                response.AddError("title", "Task title cannot be empty");
                return BadRequest(response);
            }
            var toDoAlreadyExistsInDatabase = _dataContext.ToDos.Any(x => x.TaskTitle == toDoCreateDto.TaskTitle);

            if (toDoAlreadyExistsInDatabase)
            {
                response.AddError("title", "Already exists in ToDo.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }


            var toDoToCreate = new ToDo()
            {
                TaskTitle = toDoCreateDto.TaskTitle,
                TaskDescription = toDoCreateDto.TaskDescription,
                // User = shoppingListCreateDto.User,
            };

            _dataContext.ToDos.Add(toDoToCreate);
            _dataContext.SaveChanges();

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDoToCreate.Id,
                TaskTitle = toDoToCreate.TaskTitle,
                TaskDescription = toDoCreateDto.TaskDescription
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
                .FirstOrDefault(unit => unit.Id == id);

            if (toDoToUpdate == null)
            {
                response.AddError("id", "Task not found.");
                return BadRequest(response);
            }

            toDoToUpdate.TaskTitle = toDoUpdateDto.TaskTitle;
            _dataContext.SaveChanges();

            var toDoToReturn = new ToDoGetDto
            {
                Id = toDoToUpdate.Id,
                TaskTitle = toDoToUpdate.TaskTitle,
                TaskDescription = toDoToUpdate.TaskDescription,
                CreatedDate = toDoToUpdate.CreatedDate
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
    
