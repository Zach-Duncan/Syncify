using Microsoft.AspNetCore.SignalR;
using System;

namespace LearningStarter.Entities
{
    public class ShoppingLists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        
    }

    public class ShopppingListsGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
    public class ShoppingListsCreateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
    }
    public class ShoppingListsUpdateDto
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        
    }
}
