﻿using System.Collections.Generic;
using System.Transactions;

namespace LearningStarter.Entities
{
    public class RecipeIngredient
    {
       public int Id { get; set; }
       public int RecipesId { get; set; }
       public Recipe Recipe { get; set; }
       public int IngredientsId { get; set; }
       public Ingredient Ingredient { get; set; }
       public double Quantity { get; set; }
       public int UnitId { get; set; }
       public Unit Unit { get; set; }
       
       public List<ShoppingListRecipeIngredient> ShoppingListsRecipeIngredients { get; set; } = new List<ShoppingListRecipeIngredient>();
       
    }
    public class RecipeIngredientGetDto
    {
        public int Id { get; set; }
        public RecipeGetDto Recipe { get; set; }
        public IngredientGetDto Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitGetDto Unit { get; set; }
    }
    public class RecipeIngredientUpdateDto
    {
        public int RecipeId { get; set; }
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public UnitUpdateDto Unit { get; set; }
    }
    public class RecipeIngredientCreateDto
    {
        public int RecipeId { get; set; }
        public int IngredientsId { get; set; }
        public double Quantity { get; set; }
        public UnitCreateDto Unit { get; set; }
    }
}
