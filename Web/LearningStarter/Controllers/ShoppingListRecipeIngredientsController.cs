using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Policy;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/shopping-list-recipe-ingredients")]
    public class ShoppingListRecipeIngredientsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ShoppingListRecipeIngredientsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var shoppingListRecipeIngredients = _dataContext
                .ShoppingListRecipeIngredients
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Unit)
                .Include(x => x.ShoppingList)
                .Select(shoppingListRecipeIngredients => new ShoppingListRecipeIngredientGetDto
                {
                    Id = shoppingListRecipeIngredients.Id,
                    RecipeIngredientId = shoppingListRecipeIngredients.RecipeIngredientId,
                    RecipeIngredient = new RecipeIngredientGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Id,
                        RecipeId = shoppingListRecipeIngredients.RecipeIngredientId,
                        Recipe = new RecipeGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Id,
                            Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Name,
                            Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Image,
                            Servings = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Servings,
                            Directions = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Directions,
                            MealTypeId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealTypeId,
                            MealType = new MealTypeGetDto
                            {
                                Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Id,
                                Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Name,

                            },
                            CalendarId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.CalendarId,
                            Calendar = new CalendarGetDto
                            {
                                Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Id,
                                GroupId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.GroupId,
                                Group = new GroupGetDto
                                {
                                    Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Id,
                                    Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Name,
                                    Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Image,
                                }
                            }

                        },
                        IngredientId = shoppingListRecipeIngredients.RecipeIngredient.IngredientId,
                        Ingredient = new IngredientGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Id,
                            Name = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Name,
                            Image = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Image,
                        },
                        Quantity = shoppingListRecipeIngredients.RecipeIngredient.Quantity,
                        UnitId = shoppingListRecipeIngredients.RecipeIngredient.UnitId,
                        Unit = new UnitGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Unit.Id,
                            Name = shoppingListRecipeIngredients.RecipeIngredient.Unit.Name,
                            Abbreviation = shoppingListRecipeIngredients.RecipeIngredient.Unit.Abbreviation,
                        },
                    },
                        ShoppingListId = shoppingListRecipeIngredients.ShoppingListId,
                        ShoppingList = new ShoppingListGetDto
                        {
                            Id = shoppingListRecipeIngredients.ShoppingList.Id,
                            Name = shoppingListRecipeIngredients.ShoppingList.Name,
                        },
                        Quantity = shoppingListRecipeIngredients.Quantity,

                })
                .ToList();

            response.Data = shoppingListRecipeIngredients;
            return Ok(response);

        }
        [HttpGet("{id:int}")]

        public IActionResult GetById([FromRoute] int id)
        {
            { 

            var response = new Response();

            var shoppingListRecipeIngredientsToReturn = _dataContext
            .ShoppingListRecipeIngredients
            .Include(x => x.RecipeIngredient)
            .ThenInclude(x => x.Recipe)
            .ThenInclude(x => x.MealType)
            .Include(x => x.RecipeIngredient)
            .ThenInclude(x => x.Recipe)
            .ThenInclude(x => x.Calendar)
            .ThenInclude(x => x.Group)
            .Include(x => x.RecipeIngredient)
            .ThenInclude(x => x.Ingredient)
            .Include(x => x.RecipeIngredient)
            .ThenInclude(x => x.Unit)
            .Include(x => x.ShoppingList)
            .Select(shoppingListRecipeIngredients => new ShoppingListRecipeIngredientGetDto
            {
                Id = shoppingListRecipeIngredients.Id,
                RecipeIngredientId = shoppingListRecipeIngredients.RecipeIngredientId,
                RecipeIngredient = new RecipeIngredientGetDto
                {
                    Id = shoppingListRecipeIngredients.RecipeIngredient.Id,
                    RecipeId = shoppingListRecipeIngredients.RecipeIngredientId,
                    Recipe = new RecipeGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Name,
                        Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Image,
                        Servings = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Servings,
                        Directions = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Directions,
                        MealTypeId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealTypeId,
                        MealType = new MealTypeGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Id,
                            Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Name,

                        },
                        CalendarId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.CalendarId,
                        Calendar = new CalendarGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Id,
                            GroupId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.GroupId,
                            Group = new GroupGetDto
                            {
                                Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Id,
                                Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Name,
                                Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Image,
                            }
                        }

                    },
                    IngredientId = shoppingListRecipeIngredients.RecipeIngredient.IngredientId,
                    Ingredient = new IngredientGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Name,
                        Image = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Image,
                    },
                    Quantity = shoppingListRecipeIngredients.RecipeIngredient.Quantity,
                    UnitId = shoppingListRecipeIngredients.RecipeIngredient.UnitId,
                    Unit = new UnitGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Unit.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Unit.Name,
                        Abbreviation = shoppingListRecipeIngredients.RecipeIngredient.Unit.Abbreviation,
                    },
                },
                ShoppingListId = shoppingListRecipeIngredients.ShoppingListId,
                ShoppingList = new ShoppingListGetDto
                {
                    Id = shoppingListRecipeIngredients.ShoppingList.Id,
                    Name = shoppingListRecipeIngredients.ShoppingList.Name,
                },
                Quantity = shoppingListRecipeIngredients.Quantity,

            })
            .FirstOrDefault(shoppingListRecipeIngredients => shoppingListRecipeIngredients.Id == id);

            if (!_dataContext.ShoppingListRecipeIngredients.Any(shoppingListRecipeIngredients => shoppingListRecipeIngredients.Id == id))
            {
                response.AddError("id", " Shopping List Recipe Ingredient not found, try again.");
                return BadRequest(response);
            }
            response.Data = shoppingListRecipeIngredientsToReturn;
            return Ok(response);

            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] ShoppingListRecipeIngredientCreateDto shoppingListRecipeIngredientCreateDto)
        {
            var response = new Response();

            if (!_dataContext.RecipeIngredients.Any(recipeIngredient => recipeIngredient.Id == shoppingListRecipeIngredientCreateDto.RecipeIngredientId))
            {
                response.AddError("RecipeIngredientId", "Recipe Ingredient Id cannot be found, try again.");
            }
            if (!_dataContext.ShoppingLists.Any(shoppingList => shoppingList.Id == shoppingListRecipeIngredientCreateDto.ShoppingListId))
            {
                response.AddError("ShoppingListId", "Shopping List Id cannot be found, try again.");
            }
            if (shoppingListRecipeIngredientCreateDto.Quantity <= 0)
            {
                response.AddError("Quantity", "Quantity must be greater than or equal to one");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var shoppingListRecipeIngredientToAdd = new ShoppingListRecipeIngredient
            {
                RecipeIngredientId = shoppingListRecipeIngredientCreateDto.RecipeIngredientId,
                ShoppingListId = shoppingListRecipeIngredientCreateDto.ShoppingListId,
                Quantity = shoppingListRecipeIngredientCreateDto.Quantity,
            };

            _dataContext.ShoppingListRecipeIngredients.Add(shoppingListRecipeIngredientToAdd);
            _dataContext.SaveChanges();

            var shoppingListRecipeIngredients = _dataContext
                .ShoppingListRecipeIngredients
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Unit)
                .Include(x => x.ShoppingList)
                .FirstOrDefault(x => x.Id == shoppingListRecipeIngredientToAdd.Id);
            var shoppingListRecpeIngredientsToReturn = new ShoppingListRecipeIngredientGetDto
            {
                Id = shoppingListRecipeIngredients.Id,
                RecipeIngredientId = shoppingListRecipeIngredients.RecipeIngredientId,
                RecipeIngredient = new RecipeIngredientGetDto
                {
                    Id = shoppingListRecipeIngredients.RecipeIngredient.Id,
                    RecipeId = shoppingListRecipeIngredients.RecipeIngredient.Id,
                    Recipe = new RecipeGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Name,
                        Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Image,
                        Servings = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Servings,
                        Directions = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Directions,
                        MealTypeId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealTypeId,
                        MealType = new MealTypeGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Id,
                            Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.MealType.Name,

                        },
                        CalendarId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.CalendarId,
                        Calendar = new CalendarGetDto
                        {
                            Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Id,
                            GroupId = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.GroupId,
                            Group = new GroupGetDto
                            {
                                Id = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Id,
                                Name = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Name,
                                Image = shoppingListRecipeIngredients.RecipeIngredient.Recipe.Calendar.Group.Image,
                            }
                        }
                    },
                    IngredientId = shoppingListRecipeIngredients.RecipeIngredient.IngredientId,
                    Ingredient = new IngredientGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Name,
                        Image = shoppingListRecipeIngredients.RecipeIngredient.Ingredient.Image,
                    },
                    Quantity = shoppingListRecipeIngredients.RecipeIngredient.Quantity,
                    UnitId = shoppingListRecipeIngredients.RecipeIngredient.UnitId,
                    Unit = new UnitGetDto
                    {
                        Id = shoppingListRecipeIngredients.RecipeIngredient.Unit.Id,
                        Name = shoppingListRecipeIngredients.RecipeIngredient.Unit.Name,
                        Abbreviation = shoppingListRecipeIngredients.RecipeIngredient.Unit.Abbreviation,
                    },
                },
                ShoppingListId = shoppingListRecipeIngredients.ShoppingListId,
                ShoppingList = new ShoppingListGetDto
                {
                    Id = shoppingListRecipeIngredients.ShoppingList.Id,
                    Name = shoppingListRecipeIngredients.ShoppingList.Name,
                },
                Quantity = shoppingListRecipeIngredients.Quantity,
            };
            response.Data = shoppingListRecpeIngredientsToReturn;
            return Created("", response);

        }
        [HttpPut("{id:int}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] ShoppingListRecipeIngredientUpdateDto shoppingListRecipeIngredientUpdateDto)
        {
            var response = new Response();

            var shoppingListRecipeIngredientToUpdate = _dataContext
                .ShoppingListRecipeIngredients
                .FirstOrDefault(shoppingListRecipeIngredient => shoppingListRecipeIngredient.Id == id);

            if (shoppingListRecipeIngredientToUpdate == null)
            {
                response.AddError("id", "Shopping List Recipe Ingredient Id not found");
                return BadRequest(response);
            }
            if (!_dataContext.RecipeIngredients.Any(recipeIngredient => recipeIngredient.Id == shoppingListRecipeIngredientToUpdate.RecipeIngredientId))
            {
                response.AddError("RecipeIngredientId", "Recipe Ingredient Id cannot be found, try again.");
            }
            if (!_dataContext.ShoppingLists.Any(shoppingList => shoppingList.Id == shoppingListRecipeIngredientToUpdate.ShoppingListId))
            {
                response.AddError("ShoppingListId", "Shopping List Id cannot be found, try again.");
            }
            if (shoppingListRecipeIngredientToUpdate.Quantity <= 0)
            {
                response.AddError("Quantity", "Quantity must be greater than or equal to one");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            shoppingListRecipeIngredientToUpdate.RecipeIngredientId = shoppingListRecipeIngredientUpdateDto.RecipeIngredientId;
            shoppingListRecipeIngredientToUpdate.ShoppingListId = shoppingListRecipeIngredientUpdateDto.ShoppingListId;
            shoppingListRecipeIngredientToUpdate.Quantity = shoppingListRecipeIngredientUpdateDto.Quantity;

            _dataContext.SaveChanges();

            var shoppingListRecipeIngredient = _dataContext
                .ShoppingListRecipeIngredients
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.MealType)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Recipe)
                .ThenInclude(x => x.Calendar)
                .ThenInclude(x => x.Group)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Ingredient)
                .Include(x => x.RecipeIngredient)
                .ThenInclude(x => x.Unit)
                .Include(x => x.ShoppingList)
                .FirstOrDefault(x => x.Id == shoppingListRecipeIngredientToUpdate.Id);

            var shoppingListRecipeIngredientToReturn = new ShoppingListRecipeIngredientGetDto
            {
                Id = shoppingListRecipeIngredient.Id,
                RecipeIngredientId = shoppingListRecipeIngredient.Id,
                RecipeIngredient = new RecipeIngredientGetDto
                {
                    Id = shoppingListRecipeIngredient.RecipeIngredient.Id,
                    RecipeId = shoppingListRecipeIngredient.RecipeIngredient.Id,
                    Recipe = new RecipeGetDto
                    {
                        Id = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Id,
                        Name = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Name,
                        Image = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Image,
                        Servings = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Servings,
                        Directions = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Directions,
                        MealTypeId = shoppingListRecipeIngredient.RecipeIngredient.Recipe.MealTypeId,
                        MealType = new MealTypeGetDto
                        {
                            Id = shoppingListRecipeIngredient.RecipeIngredient.Recipe.MealType.Id,
                            Name = shoppingListRecipeIngredient.RecipeIngredient.Recipe.MealType.Name,

                        },
                        CalendarId = shoppingListRecipeIngredient.RecipeIngredient.Recipe.CalendarId,
                        Calendar = new CalendarGetDto
                        {
                            Id = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Calendar.Id,
                            GroupId = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Calendar.GroupId,
                            Group = new GroupGetDto
                            {
                                Id = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Calendar.Group.Id,
                                Name = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Calendar.Group.Name,
                                Image = shoppingListRecipeIngredient.RecipeIngredient.Recipe.Calendar.Group.Image,
                            }
                        }

                    },
                    IngredientId = shoppingListRecipeIngredient.RecipeIngredient.IngredientId,
                    Ingredient = new IngredientGetDto
                    {
                        Id = shoppingListRecipeIngredient.RecipeIngredient.Ingredient.Id,
                        Name = shoppingListRecipeIngredient.RecipeIngredient.Ingredient.Name,
                        Image = shoppingListRecipeIngredient.RecipeIngredient.Ingredient.Image,
                    },
                    Quantity = shoppingListRecipeIngredient.RecipeIngredient.Quantity,
                    UnitId = shoppingListRecipeIngredient.RecipeIngredient.UnitId,
                    Unit = new UnitGetDto
                    {
                        Id = shoppingListRecipeIngredient.RecipeIngredient.Unit.Id,
                        Name = shoppingListRecipeIngredient.RecipeIngredient.Unit.Name,
                        Abbreviation = shoppingListRecipeIngredient.RecipeIngredient.Unit.Abbreviation,
                    },
                },
                ShoppingListId = shoppingListRecipeIngredient.ShoppingListId,
                ShoppingList = new ShoppingListGetDto
                {
                    Id = shoppingListRecipeIngredient.ShoppingList.Id,
                    Name = shoppingListRecipeIngredient.ShoppingList.Name,
                },
                Quantity = shoppingListRecipeIngredient.Quantity,
            };
            response.Data = shoppingListRecipeIngredientToReturn;
            return Ok(response);
        }
        [HttpDelete("{id:int}")] 

        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var shoppingListRecipeIngredientsToDelete = _dataContext
                .ShoppingListRecipeIngredients
                .FirstOrDefault(shoppingListRecipeIngredients => shoppingListRecipeIngredients.Id == id);
            if (shoppingListRecipeIngredientsToDelete == null)
            {
                response.AddError("id", "Shopping List Recipe Ingredient not found");
                return BadRequest(response);
            }
            _dataContext.Remove(shoppingListRecipeIngredientsToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}
