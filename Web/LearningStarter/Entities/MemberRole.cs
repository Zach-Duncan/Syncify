namespace LearningStarter.Entities
{
    public class MemberRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MemberRoleGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MemberRoleCreateDto
    {
        public string Name { get; set; }

    }

    public class MemberRoleUpdateDto
    {
        public string Name { get; set; }
    }
}
