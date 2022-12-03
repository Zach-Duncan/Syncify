using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/group-members")]
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
                        ProfileColor = new ProfileColorGetDto
                        {
                            Id = groupMember.User.ProfileColorId,
                            Colors = groupMember.User.ProfileColor.Colors,
                        },
                        FirstName = groupMember.User.FirstName,
                        LastName = groupMember.User.LastName,
                        PhoneNumber = groupMember.User.PhoneNumber,
                        Email = groupMember.User.Email,
                        Username = groupMember.User.Username,
                        Birthday = groupMember.User.Birthday
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
                        ProfileColor = new ProfileColorGetDto
                        {
                            Id = groupMember.User.ProfileColorId,
                            Colors = groupMember.User.ProfileColor.Colors,
                        },
                        FirstName = groupMember.User.FirstName,
                        LastName = groupMember.User.LastName,
                        PhoneNumber = groupMember.User.PhoneNumber,
                        Email = groupMember.User.Email,
                        Username = groupMember.User.Username,
                        Birthday = groupMember.User.Birthday
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
        [HttpPost]

        public IActionResult Create([FromBody] GroupMemberCreateDto groupMemberCreateDto)
        {
            var response = new Response();

            if (!_dataContext.Users.Any(user => user.Id == groupMemberCreateDto.UserId))
            {
                response.AddError("UserId", "UserId does not exist.");
            }
            if (!_dataContext.MemberRoles.Any(memberRole => memberRole.Id == groupMemberCreateDto.MemberRoleId))
            {
                response.AddError("MemberRole", "Member role does not exist.");
            }
            if (!_dataContext.Groups.Any(group => group.Id == groupMemberCreateDto.GroupId))
            {
                response.AddError("GroupId", "GroupId does not exist.");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var groupMemberToAdd = new GroupMember
            {
                UserId = groupMemberCreateDto.UserId,
                MemberRoleId = groupMemberCreateDto.MemberRoleId,
                GroupId = groupMemberCreateDto.GroupId,
            };

            _dataContext.GroupMembers.Add(groupMemberToAdd);
            _dataContext.SaveChanges();

            var groupMember = _dataContext
                .GroupMembers
                .Include(x => x.User)
                .ThenInclude(x => x.ProfileColor)
                .Include(x => x.MemberRole)
                .Include(x => x.Group)
                .FirstOrDefault(x => x.Id == groupMemberToAdd.Id);

            var groupMemberToReturn = new GroupMemberGetDto
            {
                Id = groupMember.Id,
                UserId = groupMember.UserId,
                User = new UserGetDto
                {
                    Id = groupMember.UserId,
                    ProfileColorId = groupMember.User.ProfileColorId,
                    ProfileColor = new ProfileColorGetDto
                    {
                        Id = groupMember.User.ProfileColorId,
                        Colors = groupMember.User.ProfileColor.Colors

                    },
                    FirstName = groupMember.User.FirstName,
                    LastName = groupMember.User.LastName,
                    PhoneNumber = groupMember.User.PhoneNumber,
                    Email = groupMember.User.Email,
                    Username = groupMember.User.Username,
                    Birthday = groupMember.User.Birthday
                },
                MemberRoleId = groupMember.MemberRoleId,
                MemberRole = new MemberRoleGetDto
                {
                    Id = groupMember.MemberRoleId,
                    Name = groupMember.MemberRole.Name
                },
                GroupId = groupMember.GroupId,
                Group = new GroupGetDto
                {
                    Id = groupMember.Group.Id,
                    Name = groupMember.Group.Name,
                    Image = groupMember.Group.Image,
                }
            };

            response.Data = groupMemberToReturn;
            return Created("", response);
        }
        [HttpPut("{id}")]

        public IActionResult Update(
              [FromRoute] int id,
              [FromBody] GroupMemberUpdateDto groupMemberUpdateDto)
        {
            var response = new Response();

            var groupMemberToUpdate = _dataContext
                .GroupMembers
                .FirstOrDefault(groupMember => groupMember.Id == id);

            if (groupMemberToUpdate == null)
            {
                response.AddError("id", "GroupMember not found.");
                return BadRequest(response);
            }

            groupMemberToUpdate.UserId = groupMemberUpdateDto.UserId;
            groupMemberToUpdate.MemberRoleId = groupMemberUpdateDto.MemberRoleId;
            groupMemberToUpdate.GroupId = groupMemberUpdateDto.GroupId;

            _dataContext.SaveChanges();

            var groupMember = _dataContext
                .GroupMembers
                .Include(x => x.User)
                .ThenInclude(x => x.ProfileColor)
                .Include(x => x.MemberRole)
                .Include(x => x.Group)
                .FirstOrDefault(x => x.Id == groupMemberToUpdate.Id);

            var GroupMemberToReturn = new GroupMemberGetDto
            {
                Id = groupMember.Id,
                UserId = groupMember.UserId,
                User = new UserGetDto
                {
                    Id = groupMember.UserId,
                    ProfileColorId = groupMember.User.ProfileColorId,
                    ProfileColor = new ProfileColorGetDto
                    {
                        Id = groupMember.User.ProfileColorId,
                        Colors = groupMember.User.ProfileColor.Colors
                    },
                    FirstName = groupMember.User.FirstName,
                    LastName = groupMember.User.LastName,
                    PhoneNumber = groupMember.User.PhoneNumber,
                    Email = groupMember.User.Email,
                    Username = groupMember.User.Username,
                    Birthday = groupMember.User.Birthday
                },
                MemberRoleId = groupMember.MemberRoleId,
                MemberRole = new MemberRoleGetDto
                {
                    Id = groupMember.MemberRoleId,
                    Name = groupMember.MemberRole.Name
                },
                GroupId = groupMember.GroupId,
                Group = new GroupGetDto
                {
                    Id = groupMember.GroupId,
                    Name = groupMember.Group.Name,
                    Image = groupMember.Group.Image,
                }
            };

            response.Data = GroupMemberToReturn;
            return Ok(response);
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();

            var groupMemberToDelete = _dataContext
                .GroupMembers
                .FirstOrDefault(groupMember => groupMember.Id == id);
            if (groupMemberToDelete == null)
            {
                response.AddError("id", "Group member not found.");
                return BadRequest(response);
            }

            _dataContext.Remove(groupMemberToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }

}
