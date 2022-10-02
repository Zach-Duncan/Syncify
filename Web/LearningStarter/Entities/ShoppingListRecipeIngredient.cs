namespace LearningStarter.Entities
{
    public class ShoppingListRecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public RecipeIngredient RecipeIngredient { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientGetDto
    {  
        public int Id { get; set; }
        public int RecipeIngredientId { get; set; }
        public RecipeIngredientGetDto RecipeIngredient { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingListGetDto ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientCreateDto
    {
        public int RecipeIngredientId { get; set; }
        public RecipeIngredientCreateDto RecipeIngredient { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingListCreateDto ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
    public class ShoppingListRecipeIngredientUpdateDto
    {
        public int RecipeIngredientId { get; set; }
        public RecipeIngredientUpdateDto RecipeIngredient { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingListUpdateDto ShoppingList { get; set; }
        public double Quantity { get; set; }
    }
}
