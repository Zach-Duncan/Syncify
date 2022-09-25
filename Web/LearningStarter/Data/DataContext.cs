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
        public DbSet<MemberRoles> MemberRoles { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<ProfileColors> ProfileColors { get; set; }
        public DbSet<ToDos> ToDosm { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Calendars> Calendars { get; set; }
        public DbSet<MealTypes> MealTypes { get; set; }
        public DbSet<ShoppingLists> ShoppingLists { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<ShoppingListRecipeIngredients> ShoppingListRecipeIngredients { get; set; }
        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Units> Units { get; set; }

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
