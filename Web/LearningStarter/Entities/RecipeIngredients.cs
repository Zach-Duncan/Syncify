using System.Transactions;

namespace LearningStarter.Entities
{
    public class RecipeIngredients
    {
       public int Id { get; set; }
       public int IngredientsId { get; set; }
       public double Quantity { get; set; }
        public int UnitId { get; set; }
        
    }
    public class RecipeIngredientsGetDto
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
    public class RecipeIngredientsUpdateDto
    {
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
    public class RecipeIngredientsCreateDto
    {
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
    }
}
