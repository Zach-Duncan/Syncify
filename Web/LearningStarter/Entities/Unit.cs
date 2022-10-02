using System;
using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    }

    public class UnitGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class UnitCreateDto
    {
       
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class UnitUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
