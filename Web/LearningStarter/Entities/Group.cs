namespace LearningStarter.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupCreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class GroupDeleteDto 
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
