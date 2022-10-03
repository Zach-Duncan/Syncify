
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LearningStarter.Entities
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int ProfileColorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BirthDay { get; set; }
        public string DateInput { get; set; }
    }

    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DayOfMonth { get; set; }
        public string Month { get; set; }
        //public string BirthYear { get; set; }
        public string BirthDay { get; set; }
        //public string DateInput { get; set; }
    }

    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
    }

    public class UserGetDto
    {
        public int Id { get; set; }
        public int ProfileColorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string BirthDay { get; set; }        
    }
}

/*using System;

namespace LearningStarter.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int ProfileColorId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDay { get; set; }
    }
    public class UserGetDto
    {
        public int Id { get; set; }
        public int ProfileColorId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset BirthDay { get; set; }

        public class UserCreateDto
        {
            public int Id { get; set; }
            public int ProfileColorId { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int PhoneNumber { get; set; }
            public string Email { get; set; }
            public DateTimeOffset BirthDay { get; set; }
        }
        public class UserUpdateDto
        {
            public int ProfileColorId { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int PhoneNumber { get; set; }
            public string Email { get; set; }
            public DateTimeOffset BirthDay { get; set; }
        }
    }
}
*/