using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public string Directions { get; set; }
        public int MealTypeId { get; set; }
        public MealType MealType { get; set; }
        public int CalendarId { get; set; }
        public Calendar Calendar { get; set; }
        
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }

    public class RecipeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public string Directions { get; set; }
        public int MealTypeId { get; set; }
        public MealTypeGetDto MealType { get; set; }
        public int CalendarId { get; set; }
        public CalendarGetDto Calendar { get; set; }
    }

    public class RecipeCreateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public string Directions { get; set; }
        public int MealTypeId { get; set; }
        public MealTypeCreateDto MealType { get; set; }
        public int CalendarId { get; set; }
        public CalendarCreateDto Calendar { get; set; }
    }

    public class RecipeUpdateDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public string Directions { get; set; }
        public int MealTypeId { get; set; }
        public MealTypeUpdateDto MealType { get; set; }
        public int CalendarId { get; set; }
        public CalendarUpdateDto Calendar { get; set; }
    }

}