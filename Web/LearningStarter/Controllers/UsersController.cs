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
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();

            response.Data = _context
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
        public IActionResult GetById(
            [FromRoute] int id)
        {
            var response = new Response();

            var users = _context
                .Users
                .Include(x => x.ProfileColor)
                .FirstOrDefault(x => x.Id == id);

            if (users == null)
            {
                response.AddError("id", "There was a problem finding the user.");
                return NotFound(response);
            }

            var userGetDto = new UserGetDto
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

            };          
            response.Data = userGetDto;

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(
            [FromBody] UserCreateDto userCreateDto)
        {
            var response = new Response();

            if (!_context.ProfileColors.Any(profileColor => profileColor.Id == userCreateDto.ProfileColorId))
            {
                response.AddError("ProfileColorId", "Profile Colord Id does not exist");
            } 

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
                response.AddError("email", "Email cannot be empty.");
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

            var userToCreate = new User
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

            _context.Users.Add(userToCreate);
            _context.SaveChanges();

            var users = _context
                .Users
                .Include(x => x.ProfileColor)
                .FirstOrDefault(x => x.Id == userToCreate.Id);

            var userGetDto = new UserGetDto
            {
                Id = userToCreate.Id,
                ProfileColorId = userToCreate.ProfileColorId,
                ProfileColor = new ProfileColorGetDto
                {
                    Id = userToCreate.ProfileColorId,
                    Colors = userToCreate.ProfileColor.Colors
                },
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Username = userToCreate.Username,
                Email = userToCreate.Email,
                PhoneNumber = userToCreate.PhoneNumber,
                BirthDay = userToCreate.BirthDay

            };

            response.Data = userGetDto;
            return Created("", response);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(
            [FromRoute] int id, 
            [FromBody] UserUpdateDto userUpdateDto)
        {
            var response = new Response();

            var users = _context
                .Users
                .FirstOrDefault(x => x.Id == id);

            if (users == null)
            {
                response.AddError("id", "There was a problem editing the user.");
                return NotFound(response);
            }

            var userToUpdate = _context
                .Users
                .Include(x => x.ProfileColor)
                .FirstOrDefault(x => x.Id == id);

            if (userToUpdate == null)
            {
                response.AddError("id", "Could not find user to edit.");
                return NotFound(response);
            }

            if (users.ProfileColor == null)
            {
                response.AddError("profileColor", "Profile Color cannot be empty.");
            }

            if (users.FirstName == null || users.FirstName == "")
            {
                response.AddError("firstName", "First name cannot be empty.");
            }

            if (users.LastName == null || users.LastName == "")
            {
                response.AddError("lastName", "Last name cannot be empty.");
            }

            if (users.Email == null || users.Email == "")
            {
                response.AddError("email", "Email cannot be empty.");
            }
            
            if (users.PhoneNumber.Length < 10 ) 
            {
                response.AddError("phoneNumber", "Phone number must be 10 or more digits.");
            }

            if (users.PhoneNumber == "")
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
            userToUpdate.Username = userUpdateDto.Username;
            userToUpdate.Password = userUpdateDto.Password;
            userToUpdate.PhoneNumber = userUpdateDto.PhoneNumber;
            userToUpdate.Email = userUpdateDto.Email;

            _context.SaveChanges();
            
            var user = _context
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
                BirthDay = user.BirthDay

            };

            response.Data = userToReturn;

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();

            var userToDelete = _context
                .Users
                .FirstOrDefault(user => user.Id == id);

            if (userToDelete == null)
            {
                response.AddError("id", "There was a problem deleting the user.");
                return NotFound(response);
            }

            _context.Remove(userToDelete);
            _context.SaveChanges();

            response.Data = true;
            return Ok(response);
        }
    }
}