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
                    Directions = recipes.Directions,
                    Servings = recipes.Servings,
                    MealTypeId = recipes.MealTypeId,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipes.MealTypeId,
                        Name = recipes.MealType.Name,
                    },
                    CalendarId = recipes.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = recipes.CalendarId,
                        GroupId = recipes.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = recipes.Calendar.GroupId,
                            Name = recipes.Calendar.Group.Name,
                            Image = recipes.Calendar.Group.Image
                        }
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
                    Directions = recipes.Directions,
                    Servings = recipes.Servings,
                    MealTypeId = recipes.MealTypeId,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipes.MealTypeId,
                        Name = recipes.MealType.Name,
                    },
                    CalendarId = recipes.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = recipes.CalendarId,
                        GroupId = recipes.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = recipes.Calendar.GroupId,
                            Name = recipes.Calendar.Group.Name,
                            Image = recipes.Calendar.Group.Image
                        }
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

            if (string.IsNullOrEmpty(recipeCreateDto.Image))
            {
                response.AddError("Image", "Image cannot be empty.");
            }

            if (recipeCreateDto.Servings <= 0)
            {
                response.AddError("Servings", "Servings has to be greater than 0.");
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
                Image = recipeCreateDto.Image,
                Servings = recipeCreateDto.Servings,
                Directions = recipeCreateDto.Directions,
                MealTypeId = recipeCreateDto.MealTypeId,
                CalendarId = recipeCreateDto.CalendarId
            };

            _dataContext.Recipes.Add(recipeToAdd);
            _dataContext.SaveChanges();

            var recipe = _dataContext
                .Recipes
                .Include(x => x.MealType)
                .Include(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .FirstOrDefault(x => x.Id == recipeToAdd.Id);
  
            var recipeToReturn = new RecipeGetDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Image = recipe.Image,
                Servings = recipe.Servings,
                Directions = recipe.Directions,
                MealTypeId = recipe.MealTypeId,
                MealType = new MealTypeGetDto
                {
                    Id = recipe.MealTypeId,
                    Name = recipe.MealType.Name
                },
                CalendarId = recipe.CalendarId,
                Calendar = new CalendarGetDto
                {
                    Id = recipe.CalendarId,
                    GroupId = recipe.Calendar.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = recipe.Calendar.GroupId,
                        Name = recipe.Calendar.Group.Name,
                        Image = recipe.Calendar.Group.Image
                    }
                }
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
            }

            if (string.IsNullOrEmpty(recipeToUpdate.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }

            if (string.IsNullOrEmpty(recipeToUpdate.Image))
            {
                response.AddError("Image", "Image cannot be empty.");
            }

            if (recipeToUpdate.Servings <= 0)
            {
                response.AddError("Servings", "Servings has to be greater than 0.");
            }

            if (!_dataContext.MealTypes.Any(mealType => mealType.Id == recipeToUpdate.MealTypeId))
            {
                response.AddError("MealTypeId", "Meal Type does not exist.");
            }

            if (!_dataContext.Calendars.Any(calendar => calendar.Id == recipeToUpdate.CalendarId))
            {
                response.AddError("CalendarId", "Calendar does not exist.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            recipeToUpdate.Name = recipeUpdateDto.Name;
            recipeToUpdate.Image = recipeUpdateDto.Image;
            recipeToUpdate.Servings = recipeUpdateDto.Servings;
            recipeToUpdate.Directions = recipeUpdateDto.Directions;
            recipeToUpdate.MealTypeId = recipeUpdateDto.MealTypeId;
            recipeToUpdate.CalendarId = recipeUpdateDto.CalendarId;
            
            _dataContext.SaveChanges();

            var recipe = _dataContext
                .Recipes
                .Include(x => x.MealType)
                .Include(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .FirstOrDefault(x => x.Id == recipeToUpdate.Id);

            var recipeToReturn = new RecipeGetDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Image = recipe.Image,
                Servings = recipe.Servings,
                Directions = recipe.Directions,
                MealTypeId = recipe.MealTypeId,
                MealType = new MealTypeGetDto
                {
                    Id = recipe.MealTypeId,
                    Name = recipe.MealType.Name
                },
                CalendarId = recipe.CalendarId,
                Calendar = new CalendarGetDto
                {
                    Id = recipe.CalendarId,
                    GroupId = recipe.Calendar.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = recipe.Calendar.GroupId,
                        Name = recipe.Calendar.Group.Name,
                        Image = recipe.Calendar.Group.Image
                    }
                }
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
