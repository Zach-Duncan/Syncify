using System;
using System.Linq;
using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningStarter.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public object userCreateDto { get; private set; }

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            response.Data = _dataContext
                .Users
                .Select(users => new UserGetDto
                {
                    Id = users.Id,
                    ProfileColorId = users.ProfileColorId,
                    ProfileColor = new ProfileColorGetDto
                    {
                        Id = users.ProfileColorId,
                        Colors = users.ProfileColor.Colors
                    },
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Username = users.Username,
                    PhoneNumber = users.PhoneNumber,
                    Email = users.Email,
                    BirthDay = users.BirthDay,

                })
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var response = new Response();

            var userToReturn = _dataContext
                .Users
                .Select(users => new UserGetDto
                {
                    Id = users.Id,
                    ProfileColorId = users.ProfileColorId,
                    ProfileColor = new ProfileColorGetDto
                    {
                        Id = users.ProfileColorId,
                        Colors = users.ProfileColor.Colors
                    },
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Username = users.Username,
                    PhoneNumber = users.PhoneNumber,
                    Email = users.Email,
                    BirthDay = users.BirthDay
                })
                .FirstOrDefault(users => users.Id == id);

            if (userToReturn == null)
            {
                response.AddError("id", "There was a problem finding the user.");
                return NotFound(response);
            }            

            response.Data = userToReturn;
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserCreateDto userCreateDto)
        {
            var response = new Response();

            if (userCreateDto.FirstName == null || userCreateDto.FirstName == "")
            {
                response.AddError("firstName", "First name cannot be empty.");
            }
            if (userCreateDto.LastName == null || userCreateDto.LastName == "")
            {
                response.AddError("lastName", "Last name cannot be empty.");
            }
            if (userCreateDto.Username == null || userCreateDto.Username == "")
            {
                response.AddError("userName", "User name cannot be empty.");
            }
            if (userCreateDto.Password == null || userCreateDto.Password == "")
            {
                response.AddError("password", "Password cannot be empty.");
            }
            if (userCreateDto.Email == null || userCreateDto.Email == "")
            {
                response.AddError("email","Email cannot be empty.");
            }
            if (userCreateDto.PhoneNumber == null || userCreateDto.PhoneNumber == "")
            {
                response.AddError("phoneNumber", "Phone number cannot be empty.");
            }
            if (userCreateDto.PhoneNumber.Length < 10)
            {
                response.AddError("phoneNumber", "Phone number must be 10 or more digits");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            var userToAdd = new User
            {
                ProfileColorId = userCreateDto.ProfileColorId,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Username = userCreateDto.Username,
                Password = userCreateDto.Password,
                BirthDay = userCreateDto.BirthDay,
                Email = userCreateDto.Email,
                PhoneNumber = userCreateDto.PhoneNumber,
            };

            _dataContext.Users.Add(userToAdd);
            _dataContext.SaveChanges();

            var users = _dataContext
                .Users
                .Include(x => x.ProfileColor)
                .FirstOrDefault(x => x.Id == userToAdd.Id);

            var userToReturn = new UserGetDto()
            {
                Id = users.Id,
                ProfileColorId = users.ProfileColorId,
                ProfileColor = new ProfileColorGetDto
                {
                    Id = users.ProfileColorId,
                    Colors = users.ProfileColor.Colors
                },
                FirstName = users.FirstName,
                LastName = users.LastName,
                Username = users.Username,
                Email = users.Email,
                PhoneNumber = users.PhoneNumber,
                BirthDay = users.BirthDay

            };

            response.Data = userToReturn;
            return Created("", response);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(
            [FromRoute] int id, 
            [FromBody] UserUpdateDto userUpdateDto)
        {
            var response = new Response();
            var userToUpdate = _dataContext
                .Users
                .FirstOrDefault(user => user.Id == id);

            if (userToUpdate == null)
            {
                response.AddError("id", "There was a problem editing the user.");
            }
            if (userToUpdate.FirstName == null || userToUpdate.FirstName == "")
            {
                response.AddError("firstName", "First name cannot be empty.");
            }
            if (userToUpdate.LastName == null || userToUpdate.LastName == "")
            {
                response.AddError("lastName", "Last name cannot be empty.");
            }
            if (userToUpdate.Email == null || userToUpdate.Email == "")
            {
                response.AddError("email", "Email cannot be empty.");
            }
            if (userToUpdate.PhoneNumber.Length < 10 ) 
            {
                response.AddError("phoneNumber", "Phone number must be 10 or more digitsy.");
            }
            if (userToUpdate.PhoneNumber == "")
            {
                response.AddError("phoneNumber", "Phone number cannot be empty");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }

            userToUpdate.ProfileColorId = userUpdateDto.ProfileColorId;
            userToUpdate.FirstName = userUpdateDto.FirstName;
            userToUpdate.LastName = userUpdateDto.LastName;
            userToUpdate.PhoneNumber = userUpdateDto.PhoneNumber;
            userToUpdate.Email = userUpdateDto.Email;
            _dataContext.SaveChanges();

            var user = _dataContext
                .Users
                .Include(x => x.ProfileColor)
                .FirstOrDefault(x => x.Id == userToUpdate.Id);

            var userToReturn = new UserGetDto
            {
                Id = user.Id,
                ProfileColorId = user.ProfileColorId,
                ProfileColor = new ProfileColorGetDto
                {
                    Id = user.ProfileColorId,
                    Colors = user.ProfileColor.Colors
                },
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthDay = user.BirthDay,

            };

            response.Data = userToReturn;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var userToDelete = _dataContext
                .Users
                .FirstOrDefault(user => user.Id == id);

            if (userToDelete == null)
            {
                response.AddError("id", "There was a problem deleting the user.");
                return NotFound(response);
            }

            _dataContext.Remove(userToDelete);
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}
