using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/recipe-ingredients")]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public RecipeIngredientsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var recipeIngredients = _dataContext
                .RecipeIngredients
                .Include(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.Ingredient)
                .Include(x => x.Unit)
                .Select(recipeIngredients => new RecipeIngredientGetDto
                {
                    Id = recipeIngredients.Id,
                    RecipeId = recipeIngredients.RecipeId,
                    Recipe = new RecipeGetDto
                    {
                        Id = recipeIngredients.RecipeId,
                        Name = recipeIngredients.Recipe.Name,
                        Image = recipeIngredients.Recipe.Image,
                        Servings = recipeIngredients.Recipe.Servings,
                        Directions = recipeIngredients.Recipe.Directions,
                        MealTypeId = recipeIngredients.Recipe.MealTypeId,
                        MealType = new MealTypeGetDto
                        {
                            Id = recipeIngredients.Recipe.MealTypeId,
                            Name = recipeIngredients.Recipe.MealType.Name,
                        },
                        CalendarId = recipeIngredients.Recipe.CalendarId,
                        Calendar = new CalendarGetDto
                        {
                            Id = recipeIngredients.Recipe.CalendarId,
                            GroupId = recipeIngredients.Recipe.Calendar.GroupId,
                            Group = new GroupGetDto
                            {
                                Id = recipeIngredients.Recipe.Calendar.GroupId,
                                Name = recipeIngredients.Recipe.Calendar.Group.Name,
                                Image = recipeIngredients.Recipe.Calendar.Group.Image
                            }
                        }
                    },
                    IngredientId = recipeIngredients.IngredientId,
                    Ingredient = new IngredientGetDto
                    {
                        Id = recipeIngredients.IngredientId,
                        Name = recipeIngredients.Ingredient.Name,
                        Image = recipeIngredients.Ingredient.Image,
                    },
                    Quantity = recipeIngredients.Quantity,
                    UnitId = recipeIngredients.UnitId,
                    Unit = new UnitGetDto
                    {
                        Id = recipeIngredients.UnitId,
                        Name = recipeIngredients.Unit.Name,
                        Abbreviation = recipeIngredients.Unit.Abbreviation,
                    }
                })
            .ToList();

            response.Data = recipeIngredients;
            return Ok(response);
        }
        [HttpGet("{id:int}")]

        public IActionResult GetById([FromRoute] int id)
        {
            {
                var response = new Response();

                var recipeIngredientsToReturn = _dataContext
                .RecipeIngredients
                .Select(recipeIngredients => new RecipeIngredientGetDto
                {
                    Id = recipeIngredients.Id,
                    RecipeId = recipeIngredients.RecipeId,
                    Recipe = new RecipeGetDto
                    {
                        Id = recipeIngredients.RecipeId,
                        Name = recipeIngredients.Recipe.Name,
                        Image = recipeIngredients.Recipe.Image,
                        Servings = recipeIngredients.Recipe.Servings,
                        Directions = recipeIngredients.Recipe.Directions,
                        MealTypeId = recipeIngredients.Recipe.MealTypeId,
                        MealType = new MealTypeGetDto
                        {
                            Id = recipeIngredients.Recipe.MealTypeId,
                            Name = recipeIngredients.Recipe.MealType.Name,
                        },
                        CalendarId = recipeIngredients.Recipe.CalendarId,
                        Calendar = new CalendarGetDto
                        {
                            Id = recipeIngredients.Recipe.CalendarId,
                            GroupId = recipeIngredients.Recipe.Calendar.GroupId,
                            Group = new GroupGetDto
                            {
                                Id = recipeIngredients.Recipe.Calendar.GroupId,
                                Name = recipeIngredients.Recipe.Calendar.Group.Name,
                                Image = recipeIngredients.Recipe.Calendar.Group.Image
                            }

                        }
                    },
                    IngredientId = recipeIngredients.IngredientId,
                    Ingredient = new IngredientGetDto
                    {
                        Id = recipeIngredients.IngredientId,
                        Name = recipeIngredients.Ingredient.Name,
                        Image = recipeIngredients.Ingredient.Image
                    },

                    Quantity = recipeIngredients.Quantity,
                    UnitId = recipeIngredients.UnitId,
                    Unit = new UnitGetDto
                    {
                        Id = recipeIngredients.UnitId,
                        Name = recipeIngredients.Unit.Name,
                        Abbreviation = recipeIngredients.Unit.Abbreviation,
                    }

                })
                .FirstOrDefault(recipeIngredients => recipeIngredients.Id == id);

                if (recipeIngredientsToReturn == null)
                {
                    response.AddError("id", " RecipeIngredient not found");
                    return BadRequest(response);
                }

                response.Data = recipeIngredientsToReturn;
                return Ok(response);
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] RecipeIngredientCreateDto recipeIngredientCreateDto)
        {
            var response = new Response();

            if (!_dataContext.Recipes.Any(recipe => recipe.Id == recipeIngredientCreateDto.RecipeId))
            {
                response.AddError("RecipeId", "Recipe id cannot be found, try again.");
            }

            if (!_dataContext.Ingredients.Any(ingredient => ingredient.Id == recipeIngredientCreateDto.IngredientId))
            {
                response.AddError("IngredientId", "Ingredient id cannot be found, try again.");
            }
            if (recipeIngredientCreateDto.Quantity <= 0)
            {
                response.AddError("Quantity", "Quantity must be greater than or equal to one");
            }
            if (!_dataContext.Units.Any(unit => unit.Id == recipeIngredientCreateDto.UnitId))
            {
                response.AddError("UnitId", "Unit id cannot be found, try again.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var recipeIngredientToAdd = new RecipeIngredient
            {
                RecipeId = recipeIngredientCreateDto.RecipeId,
                IngredientId = recipeIngredientCreateDto.IngredientId,
                Quantity = recipeIngredientCreateDto.Quantity,
                UnitId = recipeIngredientCreateDto.UnitId,
            };

            _dataContext.RecipeIngredients.Add(recipeIngredientToAdd);
            _dataContext.SaveChanges();

            var recipeIngredients = _dataContext
                .RecipeIngredients
                .Include(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.Ingredient)
                .Include(x => x.Unit)
                .FirstOrDefault(x => x.Id == recipeIngredientToAdd.Id);

            var recipeIngredientToReturn = new RecipeIngredientGetDto
            {
                Id = recipeIngredients.Id,
                RecipeId = recipeIngredients.RecipeId,
                Recipe = new RecipeGetDto
                {
                    Id = recipeIngredients.RecipeId,
                    Name = recipeIngredients.Recipe.Name,
                    Image = recipeIngredients.Recipe.Image,
                    Servings = recipeIngredients.Recipe.Servings,
                    Directions = recipeIngredients.Recipe.Directions,
                    MealTypeId = recipeIngredients.Recipe.MealTypeId,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipeIngredients.Recipe.MealTypeId,
                        Name = recipeIngredients.Recipe.MealType.Name,
                    },
                    CalendarId = recipeIngredients.Recipe.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = recipeIngredients.Recipe.CalendarId,
                        GroupId = recipeIngredients.Recipe.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = recipeIngredients.Recipe.Calendar.GroupId,
                            Name = recipeIngredients.Recipe.Calendar.Group.Name,
                            Image = recipeIngredients.Recipe.Calendar.Group.Image
                        }
                    }
                },
                IngredientId = recipeIngredients.IngredientId,
                Ingredient = new IngredientGetDto
                {
                    Id = recipeIngredients.IngredientId,
                    Name = recipeIngredients.Ingredient.Name,
                    Image = recipeIngredients.Ingredient.Image
                },
                Quantity = recipeIngredients.Quantity,
                UnitId = recipeIngredients.UnitId,
                Unit = new UnitGetDto
                {
                    Id = recipeIngredients.UnitId,
                    Name = recipeIngredients.Unit.Name,
                    Abbreviation = recipeIngredients.Unit.Abbreviation,
                }
            };
            response.Data = recipeIngredientToReturn;
            return Created("", response);

        }
        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] RecipeIngredientUpdateDto recipeIngredientUpdateDto)
        {
            var response = new Response();

            var recipeIngredientToUpdate = _dataContext
                .RecipeIngredients
                .FirstOrDefault(x => x.Id == id);
           

            if (recipeIngredientToUpdate == null)
            {
                response.AddError("id", "Recipe Ingredient not found.");
            }
            if (!_dataContext.Ingredients.Any(ingredient => ingredient.Id == recipeIngredientUpdateDto.IngredientId))
            {
                response.AddError("IngredientId", "Ingredient id cannot be found, try again.");
            }
            if (recipeIngredientUpdateDto.Quantity <= 0)
            {
                response.AddError("Quantity", "Quantity must be greater than or equal to one");
            }
            if (!_dataContext.Units.Any(unit => unit.Id == recipeIngredientUpdateDto.UnitId))
            {
                response.AddError("UnitId", "Unit id cannot be found, try again.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            recipeIngredientToUpdate.RecipeId = recipeIngredientUpdateDto.RecipeId;
            recipeIngredientToUpdate.IngredientId = recipeIngredientUpdateDto.IngredientId;
            recipeIngredientToUpdate.Quantity = recipeIngredientUpdateDto.Quantity;
            recipeIngredientToUpdate.UnitId = recipeIngredientUpdateDto.UnitId;

            _dataContext.SaveChanges();

            var recipeIngredient = _dataContext
                .RecipeIngredients
                .Include(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.Ingredient)
                .Include(x => x.Unit)
                .FirstOrDefault(x => x.Id == recipeIngredientToUpdate.Id);

            var recipeIngredientToReturn = new RecipeIngredientGetDto
            {
                Id = recipeIngredient.Id,
                RecipeId = recipeIngredient.RecipeId,
                Recipe = new RecipeGetDto
                {
                    Id = recipeIngredient.RecipeId,
                    Name = recipeIngredient.Recipe.Name,
                    Image = recipeIngredient.Recipe.Image,
                    Servings = recipeIngredient.Recipe.Servings,
                    Directions = recipeIngredient.Recipe.Directions,
                    MealTypeId = recipeIngredient.Recipe.MealTypeId,
                    MealType = new MealTypeGetDto
                    {
                        Id = recipeIngredient.Recipe.MealTypeId,
                        Name = recipeIngredient.Recipe.MealType.Name,
                    },
                    CalendarId = recipeIngredient.Recipe.CalendarId,
                    Calendar = new CalendarGetDto
                    {
                        Id = recipeIngredient.Recipe.CalendarId,
                        GroupId = recipeIngredient.Recipe.Calendar.GroupId,
                        Group = new GroupGetDto
                        {
                            Id = recipeIngredient.Recipe.Calendar.GroupId,
                            Name = recipeIngredient.Recipe.Calendar.Group.Name,
                            Image = recipeIngredient.Recipe.Calendar.Group.Image
                        }
                    }
                },
                IngredientId = recipeIngredient.IngredientId,
                Ingredient = new IngredientGetDto
                {
                    Id = recipeIngredient.IngredientId,
                    Name = recipeIngredient.Ingredient.Name,
                    Image = recipeIngredient.Ingredient.Image
                },

                Quantity = recipeIngredient.Quantity,
                UnitId = recipeIngredient.UnitId,
                Unit = new UnitGetDto
                {
                    Id = recipeIngredient.UnitId,
                    Name = recipeIngredient.Unit.Name,
                    Abbreviation = recipeIngredient.Unit.Abbreviation,
                }
            };
            response.Data = recipeIngredientToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var recipeIngredientToDelete = _dataContext
                .RecipeIngredients
                .FirstOrDefault(recipeIngredient => recipeIngredient.Id == id);
            if (recipeIngredientToDelete == null)
            {
                response.AddError("id", "Recipe Ingredient not found.");
                return BadRequest(response);
            }
            _dataContext.Remove(recipeIngredientToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }

}

