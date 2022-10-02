using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/ProfileColor")]
    public class ProfileColorsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProfileColorsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            var profilecolor = _dataContext.ProfileColors.Select(ProfileColor => new ProfileColorGetDto
            {

                Id = ProfileColor.Id,
                Colors = ProfileColor.Colors,
            })
            .ToList();

            response.Data = profilecolor;

            return Ok(response);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();
            var ProfileColorToReturn = _dataContext
                .ProfileColors
                .Select(ProfileColor => new ProfileColorGetDto
                {
                    Id = ProfileColor.Id,
                    Colors = ProfileColor.Colors,
                })
                .FirstOrDefault(profilecolor => profilecolor.Id == id);
            if (ProfileColorToReturn == null)
            {
                response.AddError("id", "Color not found. ");
                return BadRequest(response);
            }
            response.Data = ProfileColorToReturn;
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public IActionResult
    Update([FromRoute] int id,
    [FromBody] ProfileColorUpdateDto profilecolorUpdateDto)
        {
            var response = new Response();
            var profilecolorToUpdate = _dataContext
                .ProfileColors
                .FirstOrDefault(profilecolor => profilecolor.Id == id);
            if (profilecolorToUpdate == null)
            {
                response.AddError("id", "Color Id not found.");
                return BadRequest(response);
            }
            profilecolorToUpdate.Id = profilecolorToUpdate.Id;
            profilecolorToUpdate.Colors = profilecolorToUpdate.Colors;
            _dataContext.SaveChanges();
            var profilecolorToReturn = new ProfileColorGetDto()
            {
                Id = profilecolorToUpdate.Id,
                Colors = profilecolorToUpdate.Colors,
            };

            response.Data = profilecolorToReturn;
            return Ok(response);
        }
    }
}
