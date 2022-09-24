using System;

namespace LearningStarter.Entities
{
    public class ToDos
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class ToDosGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }


    }
    public class ToDosCreateDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
    public class ToDosUpdateDto
    {
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }

}
