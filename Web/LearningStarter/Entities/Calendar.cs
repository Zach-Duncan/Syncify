using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Calendar
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        
    }
    public class CalendarGetDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public GroupGetDto Group{ get; set; }
        
    }

    public class CalendarCreateDto
    {
        public int GroupId { get; set; }
        public GroupCreateDto Group { get; set; }
        
    }

    public class CalendarUpdateDto
    {
        public int GroupId { get; set; }
        public GroupUpdateDto Group { get; set; }
        
    }
}
