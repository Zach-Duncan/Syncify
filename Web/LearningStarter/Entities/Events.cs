using System;

namespace LearningStarter.Entities
{
    public class Events
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class EventsGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class EventsCreateDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
    public class EventsUpdateDto
    {
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
