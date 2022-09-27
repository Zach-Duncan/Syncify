using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/ingredients")]
    public class IngredientsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public IngredientsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var response = new Response();

            var ingredients = _dataContext.Ingredients.Select(ingredients => new IngredientGetDto
            {
                Id = ingredients.Id,
                Name = ingredients.Name,
                Image = ingredients.Image,
                Unit = new UnitGetDto
                {
                    Id = ingredients.UnitId,
                    Name = ingredients.Unit.Name,
                    Abbreviation = ingredients.Unit.Abbreviation
                }
            })
            .ToList();

            response.Data = ingredients;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var ingredientToReturn = _dataContext
                .Ingredients
                .Select(ingredients => new IngredientGetDto
                {
                    Id = ingredients.Id,
                    Name = ingredients.Name,
                    Image = ingredients.Image,
                    Unit = new UnitGetDto
                    {
                        Id = ingredients.UnitId,
                        Name = ingredients.Unit.Name,
                        Abbreviation = ingredients.Unit.Abbreviation
                    }
                })
                .FirstOrDefault(ingredients => ingredients.Id == id);

            if (ingredientToReturn == null)
            {
                response.AddError("id", "Ingredient not found.");
                return BadRequest(response);
            }

            response.Data = ingredientToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] IngredientCreateDto ingredientCreateDto)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(ingredientCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }
            if (!_dataContext.Units.Any(unit => unit.Id == ingredientCreateDto.UnitId))
            {
                response.AddError("UnitId", "Unit does not exist.");
            }
                if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var ingredientToAdd = new Ingredient
            {
                Name = ingredientCreateDto.Name,
                UnitId = ingredientCreateDto.UnitId               
               
            };

            _dataContext.Ingredients.Add(ingredientToAdd);
            _dataContext.SaveChanges();

            var ingredientToReturn = new IngredientGetDto
            {
                Id = ingredientToAdd.Id,
                Name = ingredientToAdd.Name,
                Image = ingredientToAdd.Image,                
            };

            response.Data = ingredientToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] IngredientUpdateDto ingredientUpdateDto)
        {
            var response = new Response();

            var ingredientToUpdate = _dataContext
                .Ingredients
                .FirstOrDefault(ingredient => ingredient.Id == id);

            if (ingredientToUpdate == null)
            {
                response.AddError("id", "Ingredient not found.");
                return BadRequest(response);
            }

            var ingredientToUpdateId = new Ingredient
            {
                Name = ingredientUpdateDto.Name,
                UnitId = ingredientUpdateDto.UnitId
            };

            ingredientToUpdate.Name = ingredientUpdateDto.Name;
            _dataContext.SaveChanges();

            var ingredientToReturn = new IngredientGetDto
            {
                Id = ingredientToUpdate.Id,
                Name = ingredientToUpdate.Name,
                Image = ingredientToUpdate.Image,
                
            };

            response.Data = ingredientToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var ingredientToDelete = _dataContext
                .Ingredients
                .FirstOrDefault(ingredient => ingredient.Id == id);
            if (ingredientToDelete == null)
            {
                response.AddError("id", "Ingredient not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(ingredientToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}
