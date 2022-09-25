namespace LearningStarter.Entities
{
    public class Groups
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupsGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupsCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupsUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
