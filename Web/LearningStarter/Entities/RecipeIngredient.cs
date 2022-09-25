using System.Transactions;

namespace LearningStarter.Entities
{
    public class RecipeIngredient
    {
       public int Id { get; set; }
       public int IngredientsId { get; set; }
       public double Quantity { get; set; }
        public int UnitId { get; set; }
        
    }
    public class RecipeIngredientGetDto
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
    public class RecipeIngredientUpdateDto
    {
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
    public class RecipeIngredientCreateDto
    {
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
}
