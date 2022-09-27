using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
        public MealType MealType { get; set; }
        public int MealTypeId { get; set; }
        public Calendar Calendar { get; set; }
        public int CalendarId { get; set; }        
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }

    public class RecipeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public UnitGetDto Unit { get; set; }
        public MealTypeGetDto MealType { get; set; }
        public CalendarGetDto Calendar { get; set; }
    }

    public class RecipeCreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int UnitId { get; set; }
        public int MealTypeId { get; set; }
        public int CalendarId { get; set; }
    }

    public class RecipeUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int UnitId { get; set; }
        public int MealTypeId { get; set; }
        public int CalendarId { get; set; }
    }

}
