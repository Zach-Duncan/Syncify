namespace LearningStarter.Entities
{
    public class MealType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MealTypeGetDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
    }
    public class MealTypeCreateDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }
}
