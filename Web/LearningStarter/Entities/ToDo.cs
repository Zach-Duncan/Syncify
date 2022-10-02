using System;

namespace LearningStarter.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class ToDoGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public CalendarGetDto Calendar { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Name { get; internal set; }
        public string EventDetails { get; internal set; }
    }
    public class ToDoCreateDto
    {
        public int CalendarId { get; set; }
        public CalendarCreateDto Calendar { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
    public class ToDoUpdateDto
    {
        public int CalendarId { get; set; }
        public CalendarUpdateDto Calendar { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }

}
