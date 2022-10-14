using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/reicpe-ingredients")]
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
                .Select(recipeIngredients => new RecipeIngredientGetDto
                {
                    Id = recipeIngredients.Id,
                    Recipe = new RecipeGetDto
                    {
                        Id = recipeIngredients.Recipe.Id,
                        Name = recipeIngredients.Recipe.Name,
                        Image = recipeIngredients.Recipe.Image,
                        Servings = recipeIngredients.Recipe.Servings,
                        MealType = new MealTypeGetDto
                        {
                            Id = recipeIngredients.Recipe.MealType.Id,
                            Name = recipeIngredients.Recipe.MealType.Name,
                        },
                    },
                    Ingredient = new IngredientGetDto
                    {
                        Id = recipeIngredients.Ingredient.Id,
                        Name = recipeIngredients.Ingredient.Name,
                        Image = recipeIngredients.Ingredient.Image,
                    },
                    Quantity = recipeIngredients.Quantity,
                    Unit = new UnitGetDto
                    {
                        Id = recipeIngredients.Unit.Id,
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
                        Recipe = new RecipeGetDto
                        {
                            Id = recipeIngredients.Recipe.Id,
                            Name = recipeIngredients.Recipe.Name,
                            Image = recipeIngredients.Recipe.Image,
                            Servings = recipeIngredients.Recipe.Servings,
                            MealType = new MealTypeGetDto
                            {
                                Id = recipeIngredients.Recipe.MealType.Id,
                                Name = recipeIngredients.Recipe.MealType.Name,
                            },
                        },
                        Ingredient = new IngredientGetDto
                        {
                            Id = recipeIngredients.Ingredient.Id,
                            Name = recipeIngredients.Ingredient.Name,
                            Image = recipeIngredients.Ingredient.Image,
                        },

                        Quantity = recipeIngredients.Quantity,
                        Unit = new UnitGetDto
                        {
                            Id = recipeIngredients.Unit.Id,
                            Name = recipeIngredients.Unit.Name,
                            Abbreviation = recipeIngredients.Unit.Abbreviation,
                        }


                    })
                .FirstOrDefault(recipeIngredients => recipeIngredients.Id == id);

                if (recipeIngredientsToReturn == null)
                {
                    response.AddError("id"," RecipeIngredient not found");
                    return BadRequest(response);
                }

                response.Data = recipeIngredientsToReturn;
                return Ok(response);
            }
        }
        //[HttpPost]
        //public IActionResult Create([FromBody] RecipeIngredientCreateDto recipeIngredientCreateDto)
        //{
        //    var response = new Response();
        //    if (string.IsNullOrEmpty(recipeIngredientCreateDto.Name))
        //    {
        //        response.AddError("Name", "Name cannot be empty");
        //        return BadRequest(response);

        //    }
        //}
    }
}


