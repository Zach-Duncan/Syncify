using System;

namespace LearningStarter.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
       // public Calendar Calendar { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        //DateTime CreatedDate = new DateTime(2002, 2, 13);
    }
    public class EventGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class EventCreateDto
    {
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class EventUpdateDto
    {
        public int CalendarId { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
