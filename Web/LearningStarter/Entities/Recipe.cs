using Microsoft.EntityFrameworkCore.Storage;

namespace LearningStarter.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int UnitId { get; set; }
        public int MealTypeId { get; set; }
        public int CalendarId { get; set; }


    }
    public class RecipeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int UnitId { get; set; }
        public int MealTypeId { get; set; }
        public int CalendarId { get; set; }
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
