using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Unit Unit { get; set; }
        public int UnitId  { get; set; }        
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }

    public class IngredientGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
        public UnitGetDto Unit { get; set; }
    }

    public class IngredientCreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set ; }
        public UnitCreateDto Unit { get; set; }
    }

    public class IngredientUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int UnitId { get; set; }
        public UnitUpdateDto Unit { get; set; }
    }
}
