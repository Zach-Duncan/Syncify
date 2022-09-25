namespace LearningStarter.Entities
{
    public class ShoppingListRecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public RecipeIngredient RecipeIngredients { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientGetDto
    {  
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientCreateDto
    {
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientUpdateDto
    {
        public int RecipeIngredientId { get; set; }
        public int ShoppingListId { get; set; }
        public double Quantity { get; set; }
    }
}
