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
    [Route("api/member-roles")]
    public class MemberRolesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public MemberRolesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            var memberRoles = _dataContext
                .MemberRoles
                .Select(memberRoles => new MemberRoleGetDto
            {
                Id = memberRoles.Id,
                Name = memberRoles.Name
            })
            .ToList();

            response.Data = memberRoles;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetbyID([FromRoute] int id)
        {
            var response = new Response();

            var memberRoleToReturn = _dataContext
                .MemberRoles
                .Select(memberRoles => new MemberRoleGetDto
                {
                    Id = memberRoles.Id,
                    Name = memberRoles.Name 
                })
                .FirstOrDefault(memberRoles => memberRoles.Id == id);

            if (memberRoleToReturn == null)
            {
                response.AddError("id", "Member Role type not found.");
                return BadRequest(response);
            }

            response.Data = memberRoleToReturn;
            return Ok(response);
        }

        [HttpPost]

        public IActionResult Create([FromBody] MemberRoleCreateDto memberRoleCreateDto)
        {
            var response = new Response();

            if (string.IsNullOrEmpty(memberRoleCreateDto.Name))
            {
                response.AddError("Name", "Name cannot be empty.");
            }

            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var memberRoleToAdd = new MemberRole
            {
               
                Name = memberRoleCreateDto.Name
            };

            _dataContext.MemberRoles.Add(memberRoleToAdd);
            _dataContext.SaveChanges();

            var memberRoleToReturn = new MemberRoleGetDto
            {
                Id = memberRoleToAdd.Id,
                Name = memberRoleToAdd.Name
            };

            response.Data = memberRoleToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]

        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] MemberRoleUpdateDto memberRoleUpdateDto)
        {
            var response = new Response();

            var memberRoleToUpdate = _dataContext
                .MemberRoles
                .FirstOrDefault(memberRole => memberRole.Id == id);

            if (memberRoleToUpdate == null)
            {
                response.AddError("id", "Member Role not found.");
                return BadRequest(response);
            }

            memberRoleToUpdate.Name = memberRoleUpdateDto.Name;
            _dataContext.SaveChanges();

            var memberRoleToReturn = new MemberRoleGetDto
            {
                Id = memberRoleToUpdate.Id,
                Name = memberRoleToUpdate.Name
            };

            response.Data = memberRoleToReturn;
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var memberRoleToDelete = _dataContext
                .MemberRoles
                .FirstOrDefault(memberRole => memberRole.Id == id);
            if (memberRoleToDelete == null)
            {
                response.AddError("id", "Member Role not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(memberRoleToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }

        [HttpGet("options")]
        public IActionResult GetOptions()
        {
            var response = new Response();

            var memberRoles = _dataContext.MemberRoles
                .Select(memberRole => new OptionDto(memberRole.Name, memberRole.Id))
                .ToList();

            response.Data = memberRoles;

            return Ok(response);
        }
    }   
}
