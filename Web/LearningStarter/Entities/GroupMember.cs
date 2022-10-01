namespace LearningStarter.Entities
{
    public class GroupMember
    {
        public int Id { get; set; }
        public MemberRole RoleId { get; set; }
        public User UserId { get; set; } 
        public Group GroupId { get; set; }
    }
    public class GroupMemberGetDto {      
        public int Id { get; set; }    
        public User UserId { get; set; } 
        public MemberRole RoleId { get; set; } 
        public Group GroupId { get; internal set; }
    } 
} 
