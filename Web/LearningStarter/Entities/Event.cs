using System;

namespace LearningStarter.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class EventGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public CalendarGetDto Calendar { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class EventCreateDto
    {
        public int CalendarId { get; set; }
        public CalendarCreateDto Calendar { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class EventUpdateDto
    {
        public int CalendarId { get; set; }
        public CalendarUpdateDto Calendar { get; set; }
        public string Name { get; set; }
        public string EventDetails { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

