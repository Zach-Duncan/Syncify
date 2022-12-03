using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/units")]

    public class UnitsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public UnitsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public IActionResult GetAll()
        {
            var response = new Response();

            var units = _dataContext.Units.Select(units => new UnitGetDto
            {
                Id = units.Id,
                Name = units.Name,
                Abbreviation = units.Abbreviation,
            })
            .ToList();

            response.Data = units;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var unitToReturn = _dataContext
                .Units
                .Select(unit => new UnitGetDto
                {
                    Id = unit.Id,
                    Name = unit.Name,
                    Abbreviation = unit.Abbreviation,
                })
                .FirstOrDefault(unit => unit.Id == id);

            if (unitToReturn == null)
            {
                response.AddError("id", "Unit not found.");
                return BadRequest(response);
            }

            response.Data = unitToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] UnitCreateDto unitCreateDto)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(unitCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }
            if (string.IsNullOrEmpty(unitCreateDto.Abbreviation))
            {
                response.AddError("Abbreviation", "Abbreviation cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var unitToAdd = new Unit
            {
                Name = unitCreateDto.Name,
                Abbreviation = unitCreateDto.Abbreviation,
            };

            _dataContext.Units.Add(unitToAdd);
            _dataContext.SaveChanges();

            var unitToReturn = new UnitGetDto
            {
                Id = unitToAdd.Id,
                Name = unitToAdd.Name,
                Abbreviation = unitToAdd.Abbreviation,
            };

            response.Data = unitToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UnitUpdateDto unitUpdateDto)
        {
            var response = new Response();

            var unitToUpdate = _dataContext
                .Units
                .FirstOrDefault(unit => unit.Id == id);

            if (unitUpdateDto == null)
            {
                response.AddError("id", "That Unit was not found, try again.");
            }
            if (string.IsNullOrEmpty(unitUpdateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }
            if (string.IsNullOrEmpty(unitUpdateDto.Abbreviation))
            {
                response.AddError("Abbreviation", "Abbreviation cannot be empty.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            unitToUpdate.Name = unitUpdateDto.Name;
            unitToUpdate.Abbreviation = unitUpdateDto.Abbreviation;
            _dataContext.SaveChanges();

            var unitToReturn = new UnitGetDto
            {
                Id = unitToUpdate.Id,
                Name = unitToUpdate.Name,
                Abbreviation = unitToUpdate.Abbreviation
            };

            response.Data = unitToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var unitToDelete = _dataContext
                .Units
                .FirstOrDefault(unit => unit.Id == id);
            if (unitToDelete == null)
            {
                response.AddError("id", "Meal Type not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(unitToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }

        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();

            var units = _dataContext.Units
                .Select(units => new OptionDto(units.Name, units.Id))
                .ToList();

            response.Data = units;

            return Ok(response);
        }
    }

}
