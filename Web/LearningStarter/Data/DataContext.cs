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
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ProfileColor> ProfileColors { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingListRecipeIngredient> ShoppingListRecipeIngredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Unit> Units { get; set; }

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

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(property => property.Unit)
                .WithMany(unit => unit.RecipeIngredients)
                .HasForeignKey(fk => fk.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
