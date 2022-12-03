using System;
using System.Web;

namespace LearningStarter.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ToDoGetDto
    {
        public int Id { get; set; }
        public int CalendarId { get; set; }
        public CalendarGetDto Calendar { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ToDoCreateDto
    {
        public int CalendarId { get; set; }
        public CalendarCreateDto Calendar { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ToDoUpdateDto
    {
        public int CalendarId { get; set; }
        public CalendarUpdateDto Calendar { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class ToDoDelete 
    {
        public int CalendarId { get; set; }
        public CalendarUpdateDto calendar { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
