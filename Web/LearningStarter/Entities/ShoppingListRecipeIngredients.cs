namespace LearningStarter.Entities
{
    public class ShoppingListRecipeIngredients
    {
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public RecipeIngredients RecipeIngredients { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingLists ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoopingListRecipeIngredientsGetDto
    {  
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoopingListRecipeIngredientsCreateDto
    {
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientsUpdateDto
    {
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
}
