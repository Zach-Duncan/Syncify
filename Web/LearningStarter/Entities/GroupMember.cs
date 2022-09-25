namespace LearningStarter.Entities
{
    public class GroupMember
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int UserId { get; set; } 
        public int GroupId { get; set; }
    }
    public class GroupMemeberGetDto {      
        public int Id { get; set; }    
        public int UserId { get; set; } 
        public string RoleId { get; set; }  
    } 
} 
