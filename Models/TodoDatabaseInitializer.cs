using System;
using System.Data.Entity;

namespace Todo.Models
{
    // DEMONSTRATION/DEVELOPMENT ONLY
    public class TodoDatabaseInitializer:
        DropCreateDatabaseAlways<ShopAppContext> // re-creates every time the server starts TODO: Swap these vvv
        //DropCreateDatabaseIfModelChanges<ShopAppContext> 
    {
        protected override void Seed(ShopAppContext context)
        {
            SeedDatabase(context);
        }

        public static void SeedDatabase(ShopAppContext context)
        {
            _baseCreatedAtDate = new DateTime(2000, 1, 1, 0, 0, 0);

            var todos = new[] {
                // Description, IsDone, IsArchived
                CreateTodo("Food", true, true),
                CreateTodo("Water", true, true),
                CreateTodo("Shelter", true, true),
                CreateTodo("Bread", false, false),
                CreateTodo("Cheese", true, false),
                CreateTodo("Wine", false, false)
            };

            var stores = new[] {
                // Name, Suburb
                CreateStore("IGA", "Roleystone"), // 1
                CreateStore("Olsen Butchers", "Roleystone"), // 2
                CreateStore("City Farmers", "Kelmscott") // 3
            };

            var itemtypes = new[] {
                // Name
                CreateItemType("Fruit/Vegetables"), // 1
                CreateItemType("Snacks"), // 2
                CreateItemType("Confectionary"), // 3
                CreateItemType("Electronics"), // 4
                CreateItemType("Hygiene"), // 5
                CreateItemType("Meat"), // 6
                CreateItemType("Pet Food"), // 7
                CreateItemType("Dairy"), // 8
                CreateItemType("Nuts"), // 9
                CreateItemType("Drinks"), // 10
                CreateItemType("Pasta"), // 11
                CreateItemType("Household"), // 12
                CreateItemType("Bread"), // 13
                CreateItemType("Frozen"), // 14
                CreateItemType("Herbs"), // 15
                CreateItemType("Canned Food"), // 16
                CreateItemType("Sauces/Condiments"), // 17
                CreateItemType("Cereal"), // 18
                CreateItemType("Baking") // 19
            };

            var items = new[] {
                //            Name,    Aisle, StoreID, ItemTypeID
                CreateItem("Brocolli",          1, 1, 1),
                CreateItem("Potatoes",          1, 1, 1),
                CreateItem("Bananas",           1, 1, 1),
                CreateItem("Avocado",           1, 1, 1),
                CreateItem("Spring Onion",      1, 1, 1),
                CreateItem("Onion",             1, 1, 1),
                CreateItem("Lettuce",           1, 1, 1),
                CreateItem("Kale",              1, 1, 1),
                CreateItem("Grape Tomato",      1, 1, 1),
                CreateItem("Capsicum Red",      1, 1, 1),
                CreateItem("Capsicum Green",    1, 1, 1),
                CreateItem("Spinach Leaves",    1, 1, 1),
                CreateItem("Full Cream Milk",   1, 1, 8),
                CreateItem("Skim Milk",         1, 1, 8),
                CreateItem("Oat Milk",          1, 1, 8),
                CreateItem("Brownes Original Yogurt", 1, 1, 8),
                CreateItem("Quorn Mince",      1, 1, 14),
                CreateItem("Marinated Fetta",   1, 1, 8),
                CreateItem("Matured Cheddar Cheese", 1, 1, 8),
                CreateItem("Parmesan Cheese",   1, 1, 8),
                CreateItem("Butter",            1, 1, 8),
                CreateItem("Peas",              1, 1, 1),
                CreateItem("Crushed Tomatoes 410g",  1, 1, 16),
                CreateItem("Tomatoes",          1, 1, 1),
                CreateItem("Shredded Ham",      1, 1, 6),
                CreateItem("Bacon Pieces",      1, 1, 6),
                CreateItem("Salami",            1, 1, 6),
                CreateItem("Tuna 425g",              1, 1, 16),
                CreateItem("Pineapple Pieces",  1, 1, 16),
                CreateItem("Baked Beans",       1, 1, 16),
                CreateItem("Hazelnut Chocolate", 1, 1, 3),
                CreateItem("Top Deck Chocolate", 1, 1, 3),
                CreateItem("Fruit & Nut Chocolate", 1, 1, 3),
                CreateItem("Black Forest Chocolate", 1, 1, 3),
                CreateItem("Linguine",          1, 1, 11),
                CreateItem("Spirals",           1, 1, 11),
                CreateItem("Large Spirals",     1, 1, 11),
                CreateItem("Bowties",           1, 1, 11),
                CreateItem("Tissues",           1, 1, 5),
                CreateItem("Soap",              1, 1, 5),
                CreateItem("Exfoliating Soap",  1, 1, 5),
                CreateItem("Firelighters",      1, 1, 12),
                CreateItem("White Burger Buns", 1, 1, 13),
                CreateItem("Wholemeal Burger Buns", 1, 1, 13),
                CreateItem("White Hotdog Buns", 1, 1, 13),
                CreateItem("Wholemeal Bread",   1, 1, 13),
                CreateItem("White Bread",       1, 1, 13),
                CreateItem("Eggs",              1, 1, 8),
                CreateItem("Orange Juice",      1, 1, 10),
                CreateItem("Pure Cream",        1, 1, 8),
                CreateItem("Whipped Cream",     1, 1, 8),
                CreateItem("Cucumber",          1, 1, 1),
                CreateItem("Stilton Cheese",    1, 1, 8),
                CreateItem("Crinkle Cut Chips", 1, 1, 14),
                CreateItem("Smooth Chips",      1, 1, 14),
                CreateItem("Tomato Paste",      1, 1, 17),
                CreateItem("Brie Cheese",       1, 1, 8),
                CreateItem("Dark Chocolate",    1, 1, 3),
                CreateItem("Dairy Milk Chocolate", 1, 1, 3),
                CreateItem("Basil",             1, 1, 15),
                CreateItem("Oregano",           1, 1, 15),
                CreateItem("Almonds",           1, 1, 9),
                CreateItem("Rolled Oats",       1, 1, 18),
                CreateItem("Quick Oats",        1, 1, 18),
                CreateItem("Pigs Ear",          1, 1, 7),
                CreateItem("White Tea",         1, 1, 10), // Expand Teas
                CreateItem("Original Crisps",   1, 1, 2),
                CreateItem("Salt & Vinegar Crisps", 1, 1, 2),
                CreateItem("Dog Food",          1, 3, 7),
                CreateItem("Chook Food",        1, 3, 7)
            };

            var units = new[] {
                // Name
                CreateUnit("Kg"), // 1
                CreateUnit("") // 2
            };

            var users = new[] {
                // Name
                CreateUser("Alex", "ArmadylsMight"), // 1
                CreateUser("Mandy", "FakeLol") // 2
            };

            var requests = new[] {
                // ItemId, Amount, UnitId, Frequency(days), UserId
                CreateRequest(32, 5, 2, 7, 1), // 1
                CreateRequest(16, 1, 2, 14, 2), // 2
                CreateRequest(23, 3, 2, 0, 2), // 3
                CreateRequest(1, 1, 2, 0, 2), // 4
                CreateRequest(2, 1.5f, 1, 0, 2), // 5
                CreateRequest(3, 10, 2, 0, 2), // 6
                CreateRequest(4, 156, 2, 0, 2), // 7
                CreateRequest(5, 2, 2, 0, 2), // 8
                CreateRequest(6, 7, 2, 0, 2), // 9
                CreateRequest(7, 8, 2, 0, 2), // 10
                CreateRequest(8, 12, 2, 0, 2), // 11
                CreateRequest(9, 15, 2, 0, 2), // 12
                CreateRequest(10, 6, 2, 0, 2), // 13
                CreateRequest(11, 24, 2, 0, 2), // 14
                CreateRequest(12, 19, 2, 0, 2), // 15
                CreateRequest(13, 21, 2, 0, 2), // 16
                CreateRequest(14, 56, 2, 0, 2), // 17
                CreateRequest(15, 1267, 2, 0, 2), // 18
                CreateRequest(17, 87, 2, 0, 2), // 19
                CreateRequest(18, 24, 2, 0, 2), // 20
                CreateRequest(19, 13, 2, 0, 2), // 21
            };

            var movies = new[] {
                // Name, Sequel Number, BluRay
                CreateMovie("Iron Man", "",  1, true),
                CreateMovie("Iron Man", "2", 2, true),
                CreateMovie("Iron Man", "3",  3, true),
                CreateMovie("Avengers", "",  1, true),
                CreateMovie("Avengers", "Age of Ultron",  2, true),
                CreateMovie("Thor", "",  1, true),
                CreateMovie("Thor", "The Dark World",  2, true),
                CreateMovie("Guardians of the Galaxy", "",  1, true),
                CreateMovie("Captain America", "",  1, true),
                CreateMovie("Captain America", "The Winter Soldier",  2, true),
                CreateMovie("Arrival", "",  1, true),
                CreateMovie("Watchmen", "",  1, true),
                CreateMovie("Captain America", "Civil War",  3, true),
                CreateMovie("Hot Fuzz", "",  1, false),
                CreateMovie("Lucy", "",  1, true),
                CreateMovie("Lord of the Rings", "The Fellowship of the Ring",  1, false),
                CreateMovie("Lord of the Rings", "The Two Towers",  2, false),
                CreateMovie("Lord of the Rings", "The Return of the King",  3, false),
                CreateMovie("How to Train Your Dragon", "",  1, true),
                CreateMovie("How to Train Your Dragon", "2",  2, true)
            };

            Array.ForEach(todos, t => context.Todos.Add(t));
            Array.ForEach(stores, s => context.Stores.Add(s));
            Array.ForEach(itemtypes, it => context.ItemTypes.Add(it));
            Array.ForEach(units, un => context.Units.Add(un));
            Array.ForEach(movies, m => context.Movies.Add(m));

            context.SaveChanges(); // Initial Save

            Array.ForEach(items, i => context.Items.Add(i));
            Array.ForEach(users, us => context.Users.Add(us));

            context.SaveChanges(); // Mid Save

            Array.ForEach(requests, r => context.Requests.Add(r));

            context.SaveChanges(); // Final Save
        }

        private static TodoItem CreateTodo(string description, bool isDone, bool isArchived)
        {
            _baseCreatedAtDate = _baseCreatedAtDate.AddMinutes(1);
            return new TodoItem
            {
                CreatedAt = _baseCreatedAtDate,
                Description = description,
                IsDone = isDone,
                IsArchived = isArchived
            };
        }

        private static Store CreateStore(string name, string location)
        {
            return new Store
            {
                StoreName = name,
                StoreLoc = location
            };
        }

        private static ItemType CreateItemType(string name)
        {
            return new ItemType
            {
                ItemTypeName = name
            };
        }

        private static Item CreateItem(string name, int aisle, int store, int itemType)
        {
            return new Item
            {
                ItemName = name,
                AisleNumber = aisle,
                StoreId = store,
                ItemTypeId = itemType
            };
        }

        private static User CreateUser(string name, string pass)
        {
            return new User
            {
                UserName = name,
                UserPass = pass
            };
        }

        private static Unit CreateUnit(string name)
        {
            return new Unit
            {
                UnitName = name
            };
        }

        private static Request CreateRequest(int itemId, float amount, int unitId, int frequency, int userId)
        {
            return new Request
            {
                ItemId = itemId,
                Amount = amount,
                UnitId = unitId,
                Frequency = frequency,
                UserId = userId,
                DateAdded = DateTime.Now,
                Bought = false,
                DateBought = _baseCreatedAtDate
            };
        }

        private static Movie CreateMovie(string mainTitle, string secTitle, int sequelnum, bool bluray)
        {
            return new Movie
            {
                MainTitle = mainTitle,
                SecondaryTitle = secTitle,
                SequelNum = sequelnum,
                BluRay = bluray
            };
        }

        private static DateTime _baseCreatedAtDate;

        public static void PurgeDatabase(ShopAppContext context)
        {
            var todos = context.Todos;
            foreach (var todoItem in todos)
            {
                todos.Remove(todoItem);
            }

            context.SaveChanges();
        }

    }


}