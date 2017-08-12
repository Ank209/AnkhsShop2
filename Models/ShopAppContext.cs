namespace Todo.Models
{
    using System.Data.Entity;

    public class ShopAppContext : DbContext 
    {
        // DEVELOPMENT ONLY: initialize the database
        static ShopAppContext()
        {
            Database.SetInitializer(new TodoDatabaseInitializer());
        }
        public DbSet<TodoItem> Todos { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}