using Microsoft.AspNetCore.SignalR;
using System;

namespace LearningStarter.Entities
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        
    }

    public class ShoppingListGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
    public class ShoppingListCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
    public class ShoppingListUpdateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        
    }
}
