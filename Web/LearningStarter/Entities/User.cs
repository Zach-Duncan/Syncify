using System;

namespace LearningStarter.Entities
{
    public class UsersGetDto
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
}
