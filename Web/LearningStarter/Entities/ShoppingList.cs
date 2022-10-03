using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace LearningStarter.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<ShoppingListRecipeIngredient> ShoppingListsRecipeIngredients { get; set; } = new List<ShoppingListRecipeIngredient>();        
    }
    public class ShoppingListGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserGetDto User { get; set; }
    }
    public class ShoppingListCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserCreateDto User { get; set; }
    }
    public class ShoppingListUpdateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public UserUpdateDto User { get; set; }
        
    }
}
