using System;

namespace LearningStarter.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class ToDoGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }


    }
    public class ToDoCreateDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
    public class ToDoUpdateDto
    {
        public int CalendarId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }

}
