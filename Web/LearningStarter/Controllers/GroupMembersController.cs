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
    [Route("api/GroupMember")]
    public class GroupMemberController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public GroupMemberController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            var groupmember = _dataContext.GroupMembers.Select(GroupMember => new GroupMemberGetDto
            {

                Id = GroupMember.Id,
                RoleId = GroupMember.RoleId,
                UserId = GroupMember.UserId,
                GroupId = GroupMember.GroupId
            })
            .ToList();

            response.Data = groupmember;

            return Ok(response);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();
            var GroupMemberToReturn = _dataContext
                .GroupMembers
                .Select(GroupMember => new GroupMemberGetDto
                {
                    Id = GroupMember.Id,
                    RoleId = GroupMember.RoleId,
                    UserId = GroupMember.UserId,
                    GroupId = GroupMember.GroupId
                })
                .FirstOrDefault(group => group.Id == id);
            if (GroupMemberToReturn == null)
            {
                response.AddError("id", "Order not found. ");
                return BadRequest(response);
            }
            response.Data = GroupMemberToReturn;
            return Ok(response);
        }
    }
}
