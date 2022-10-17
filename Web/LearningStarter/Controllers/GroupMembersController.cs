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
    [Route("api/groupMember")]
    public class GroupMembersController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public GroupMembersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            var groupMember = _dataContext
                .GroupMembers
                .Select(groupMember => new GroupMemberGetDto
            {

                Id = groupMember.Id,
                MemberRoleId = groupMember.MemberRoleId,
                MemberRole = new MemberRoleGetDto
                {
                    Id = groupMember.MemberRoleId,
                    Name = groupMember.MemberRole.Name
                },
                UserId = groupMember.UserId,
                User = new UserGetDto
                {
                    Id = groupMember.UserId,
                    ProfileColorId = groupMember.User.ProfileColorId,
                    FirstName = groupMember.User.FirstName,
                    LastName = groupMember.User.LastName,
                    PhoneNumber = groupMember.User.PhoneNumber,
                    Email = groupMember.User.Email,
                    Username = groupMember.User.Username,
                    BirthDay = groupMember.User.BirthDay                    
                },
                GroupId = groupMember.GroupId,
                Group = new GroupGetDto
                {
                    Id = groupMember.Id,
                    Name = groupMember.Group.Name,
                    Image = groupMember.Group.Image
                }
            })
            .ToList();

            response.Data = groupMember;

            return Ok(response);

        }
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();
            var groupMemberToReturn = _dataContext
                .GroupMembers
                .Select(groupMember => new GroupMemberGetDto
                {
                    Id = groupMember.Id,
                    MemberRoleId = groupMember.MemberRoleId,
                    MemberRole = new MemberRoleGetDto
                    {
                        Id = groupMember.Id,
                        Name = groupMember.MemberRole.Name
                    },
                    UserId = groupMember.UserId,
                    User = new UserGetDto
                    {
                        Id = groupMember.Id,
                        ProfileColorId = groupMember.User.ProfileColorId,
                        FirstName = groupMember.User.FirstName,
                        LastName = groupMember.User.LastName,
                        PhoneNumber = groupMember.User.PhoneNumber,
                        Email = groupMember.User.Email,
                        Username = groupMember.User.Username,
                        BirthDay = groupMember.User.BirthDay
                    },
                    GroupId = groupMember.GroupId,
                    Group = new GroupGetDto
                    {
                        Id = groupMember.Id,
                        Name = groupMember.Group.Name,
                        Image = groupMember.Group.Image
                    }
                })
                .FirstOrDefault(groupMembers => groupMembers.Id == id);
            if (groupMemberToReturn == null)
            {
                response.AddError("id", "Group Member not found. ");
                return BadRequest(response);
            }
            response.Data = groupMemberToReturn;
            return Ok(response);
        }
    }
}
