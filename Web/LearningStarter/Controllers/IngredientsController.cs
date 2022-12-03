using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

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

            var ingredients = _dataContext
                .Ingredients
                .Select(ingredients => new IngredientGetDto
            {
                Id = ingredients.Id,
                Name = ingredients.Name,
                Image = ingredients.Image,
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


            if (string.IsNullOrEmpty(ingredientCreateDto.Image))
            {
                response.AddError("Image", "Image cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var ingredientToAdd = new Ingredient
            {
                Name = ingredientCreateDto.Name,

                Image = ingredientCreateDto.Image,
            };

            _dataContext.Ingredients.Add(ingredientToAdd);
            _dataContext.SaveChanges();

            var ingredient = _dataContext
                .Ingredients
                .FirstOrDefault(x => x.Id == ingredientToAdd.Id);

            var ingredientToReturn = new IngredientGetDto()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Image = ingredient.Image
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
            }

            if (string.IsNullOrEmpty(ingredientUpdateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }

            if (string.IsNullOrEmpty(ingredientUpdateDto.Image))
            {
                response.AddError("Image", "Image cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            ingredientToUpdate.Name = ingredientUpdateDto.Name;
            ingredientToUpdate.Image = ingredientUpdateDto.Image;
            _dataContext.SaveChanges();

            var ingredient = _dataContext
                .Ingredients
                .FirstOrDefault(x => x.Id == ingredientToUpdate.Id);

            var ingredientToReturn = new IngredientGetDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Image = ingredient.Image            
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

        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();

            var ingredients = _dataContext.Ingredients
                .Select(ingredients => new OptionDto(ingredients.Name, ingredients.Id))
                .ToList();

            response.Data = ingredients;

            return Ok(response);
        }
    }
}
