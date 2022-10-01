using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public RecipesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var response = new Response();

            var recipes = _dataContext
                .Recipes
                .Select(recipes => new RecipeGetDto
                {
                    Id = recipes.Id,
                    Name = recipes.Name,
                    Image = recipes.Image,
                    Servings = recipes.Servings,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipes.MealTypeId,
                        Name = recipes.MealType.Name,
                    },
                    Calendar = new CalendarGetDto
                    {
                        Id = recipes.CalendarId,
                    }
                })
            .ToList();

            response.Data = recipes;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var recipeToReturn = _dataContext
                .Recipes
                .Select(recipes => new RecipeGetDto
                {
                    Id = recipes.Id,
                    Name = recipes.Name,
                    Image = recipes.Image,
                    Servings = recipes.Servings,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipes.MealTypeId,
                        Name = recipes.MealType.Name,
                    },
                    Calendar = new CalendarGetDto
                    {
                        Id = recipes.CalendarId,
                    }
                })
                .FirstOrDefault(recipes => recipes.Id == id);

            if (recipeToReturn == null)
            {
                response.AddError("id", "Recipe not found.");
                return BadRequest(response);
            }

            response.Data = recipeToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] RecipeCreateDto recipeCreateDto)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(recipeCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }
            if (!_dataContext.MealTypes.Any(mealType => mealType.Id == recipeCreateDto.MealTypeId))
            {
                response.AddError("MealTypeId", "Meal Type does not exist.");
            }
            if (!_dataContext.Calendars.Any(calendar => calendar.Id == recipeCreateDto.CalendarId))
            {
                response.AddError("CalendarId", "Calendar does not exist.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var recipeToAdd = new Recipe
            {
                Name = recipeCreateDto.Name,
                MealTypeId = recipeCreateDto.MealTypeId,
                CalendarId = recipeCreateDto.CalendarId

            };

            _dataContext.Recipes.Add(recipeToAdd);
            _dataContext.SaveChanges();

            var recipeToReturn = new RecipeGetDto
            {
                Id = recipeToAdd.Id,
                Name = recipeToAdd.Name,
                Image = recipeToAdd.Image,
            };

            response.Data = recipeToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] RecipeUpdateDto recipeUpdateDto)
        {
            var response = new Response();

            var recipeToUpdate = _dataContext
                .Recipes
                .FirstOrDefault(recipe => recipe.Id == id);

            if (recipeToUpdate == null)
            {
                response.AddError("id", "Recipe not found.");
                return BadRequest(response);
            }

            var recipeToUpdateId = new Recipe
            {
                Name = recipeUpdateDto.Name,
                MealTypeId = recipeUpdateDto.MealTypeId,
                CalendarId = recipeUpdateDto.CalendarId
            };

            recipeToUpdate.Name = recipeUpdateDto.Name;
            _dataContext.SaveChanges();

            var recipeToReturn = new RecipeGetDto
            {
                Id = recipeToUpdate.Id,
                Name = recipeToUpdate.Name,
                Image = recipeToUpdate.Image,

            };

            response.Data = recipeToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var recipeToDelete = _dataContext
                .Recipes
                .FirstOrDefault(recipe => recipe.Id == id);
            if (recipeToDelete == null)
            {
                response.AddError("id", "Recipe not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(recipeToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}
