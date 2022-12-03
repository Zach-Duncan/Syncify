namespace LearningStarter.Entities
{
    public class GroupMember
    {
        public int Id { get; set; }
        public int MemberRoleId { get; set; }
        public MemberRole MemberRole { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        
    }
    public class GroupMemberGetDto 
    {      
        public int Id { get; set; }   
        public int UserId { set; get; }
        public UserGetDto User { get; set; }
        public int MemberRoleId { get; set; }
        public MemberRoleGetDto MemberRole { get; set; } 
        public int GroupId { get; set; }
        public GroupGetDto Group { get; set; }
    }
    
    public class GroupMemberCreateDto
    {
        public int UserId { get; set; }
        public UserCreateDto User { get; set; }
        public int MemberRoleId { get; set; }
        public MemberRoleCreateDto MemberRole { get; set; }
        public int GroupId { get; set; }
        public GroupCreateDto Group { get; set; }

    }

    public class GroupMemberUpdateDto
    {
        public int UserId { get; set; }
        public UserUpdateDto User { get; set; }
        public int MemberRoleId { get; set; }
        public MemberRoleUpdateDto MemberRole { get; set; }
        public int GroupId { get; set; }
        public GroupUpdateDto Group { get; set; }

    }
} 
