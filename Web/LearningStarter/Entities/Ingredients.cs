using Newtonsoft.Json.Converters;

namespace LearningStarter.Entities
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
    public class IngredientsGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
    public class IngredientsCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
    public class IngredientsUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
}
