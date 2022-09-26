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
        public string Name { get; set; }
    }
    public class MealTypeCreateDto
    {
        public string Name { get; set; }
    }

    public class MealTypeUpdateDto
    {
        public string Name { get; set; }
    }
}
