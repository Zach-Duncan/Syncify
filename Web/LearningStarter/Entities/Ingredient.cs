namespace LearningStarter.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId  { get; set; }
    }
    public class IngredientGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
    public class IngredientCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
    public class IngredientUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
    }
}
