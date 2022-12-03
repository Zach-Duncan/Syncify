using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/meal-types")]

    public class MealTypesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public MealTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var response = new Response();

            var mealTypes = _dataContext.MealTypes.Select(mealTypes => new MealTypeGetDto
            {
                Id = mealTypes.Id,
                Name = mealTypes.Name
            })
            .ToList();

            response.Data = mealTypes;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var mealTypeToReturn = _dataContext
                .MealTypes
                .Select(mealType => new MealTypeGetDto
                {
                    Id = mealType.Id,
                    Name = mealType.Name
                })        
                .FirstOrDefault(mealType => mealType.Id == id);

            if (mealTypeToReturn == null)
            {
                response.AddError("id", "Meal Type not found.");
                return BadRequest(response);
            }

            response.Data = mealTypeToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] MealTypeCreateDto mealTypeCreateDto)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(mealTypeCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var mealTypeToAdd = new MealType
            {
                Name = mealTypeCreateDto.Name
            };

            _dataContext.MealTypes.Add(mealTypeToAdd);
            _dataContext.SaveChanges();

            var mealTypeToReturn = new MealTypeGetDto
            {
                Id = mealTypeToAdd.Id,
                Name = mealTypeToAdd.Name
            };

            response.Data = mealTypeToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] MealTypeUpdateDto mealTypeUpdateDto)
        {
            var response = new Response();

            var mealTypeToUpdate = _dataContext
                .MealTypes
                .FirstOrDefault(mealType => mealType.Id == id);

            if (mealTypeToUpdate == null)
            {
                response.AddError("id", "Meal Type not found.");
                return BadRequest(response);
            }

            mealTypeToUpdate.Name = mealTypeUpdateDto.Name;
            _dataContext.SaveChanges();

            var mealTypeToReturn = new MealTypeGetDto
            {
                Id = mealTypeToUpdate.Id,
                Name = mealTypeToUpdate.Name
            };

            response.Data = mealTypeToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var mealTypeToDelete = _dataContext
                .MealTypes
                .FirstOrDefault(mealType => mealType.Id == id);
            if (mealTypeToDelete == null)
            {
                response.AddError("id", "Meal Type not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(mealTypeToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }


        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();

            var mealTypes = _dataContext.MealTypes
                .Select(mealTypes => new OptionDto(mealTypes.Name, mealTypes.Id))
                .ToList();

            response.Data = mealTypes;

            return Ok(response);
        }
    }

}
