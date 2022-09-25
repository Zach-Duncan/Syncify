using LearningStarter.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningStarter.Data
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MemberRole> MemberRole { get; set; }
        public DbSet<GroupMember> GroupMember { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<ProfileColor> ProfileColor { get; set; }
        public DbSet<ToDos> ToDo { get; set; }
        public DbSet<Events> Event { get; set; }
        public DbSet<Calendars> Calendar { get; set; }
        public DbSet<MealTypes> MealType { get; set; }
        public DbSet<ShoppingLists> ShoppingList { get; set; }
        public DbSet<Recipes> Recipe { get; set; }
        public DbSet<ShoppingListRecipeIngredients> ShoppingListRecipeIngredient { get; set; }
        public DbSet<RecipeIngredients> RecipeIngredient { get; set; }
        public DbSet<Ingredients> Ingredient { get; set; }
        public DbSet<Units> Unit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .IsRequired();
        }
    }
}
