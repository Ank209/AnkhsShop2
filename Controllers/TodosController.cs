using System;
using System.Linq;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;

using Todo.Models;
using Newtonsoft.Json.Linq;

namespace Todo.Controllers
{
    [BreezeController]
    public class TodosController : ApiController {
        #region stuff
        static readonly TimeSpan RefreshRate = TimeSpan.FromMinutes(60);
        private static readonly object Locker = new object();
        static DateTime _lastRefresh = DateTime.Now; // will first clear db at Now + "RefreshRate" 
        // static DateTime lastRefresh = DateTime.MinValue; // will clear when server starts
        #endregion
        readonly EFContextProvider<ShopAppContext> _contextProvider =
            new EFContextProvider<ShopAppContext>();

        public TodosController()
        {
            PeriodicReset();
        }

        // ~/breeze/todos/Metadata
        [HttpGet]
        public string Metadata() {
            return _contextProvider.Metadata();
        }

        // ~/breeze/todos/Todos
        // ~/breeze/todos/Todos?$filter=IsArchived eq false&$orderby=CreatedAt
        [HttpGet]
        public IQueryable<TodoItem> Todos() {
            //var requests = _contextProvider.Context.Requests.Include("Unit").Include("Item").Include("User").ToList();
            return _contextProvider.Context.Todos;
        }

        [HttpGet]
        public IQueryable<Item> Items()
        {
            return _contextProvider.Context.Items.Include("Store").Include("ItemType");
        }

        [HttpGet]
        public IQueryable<Request> Requests()
        {
            return _contextProvider.Context.Requests.Include("Unit").Include("Item.Store").Include("User");
        }

        [HttpGet]
        public IQueryable<Store> Stores()
        {
            return _contextProvider.Context.Stores;
        }

        [HttpGet]
        public IQueryable<ItemType> ItemTypes()
        {
            return _contextProvider.Context.ItemTypes;
        }

        [HttpGet]
        public IQueryable<Unit> Units()
        {
            return _contextProvider.Context.Units;
        }

        [HttpGet]
        public IQueryable<Movie> Movies()
        {
            return _contextProvider.Context.Movies;
        }

        [HttpGet]
        public string[] VerifyUser(string userName, string password)
        {
            var userData = _contextProvider.Context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            string[] result = new string[2] { userData == null ? "false" : userData.UserPass == password ? "true" : "false", userData == null ? null : userData.UserId.ToString() };
            return result;
        }

        // ~/breeze/todos/SaveChanges
        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle) {
            return _contextProvider.SaveChanges(saveBundle);
        }

        // ~/breeze/todos/purge
        [HttpPost]
        public string Purge()
        {
            TodoDatabaseInitializer.PurgeDatabase(_contextProvider.Context);
            return "purged";
        }

        // ~/breeze/todos/reset
        [HttpPost]
        public string Reset()
        {
            Purge();
            TodoDatabaseInitializer.SeedDatabase(_contextProvider.Context);
            return "reset";
        }

        /// <summary>
        /// Reset the database to it's initial data state after the server has run 
        /// for "RefreshRate" minutes.
        /// </summary>
        private void PeriodicReset()
        {
            if ((DateTime.Now - _lastRefresh) > RefreshRate)
            {
                lock (Locker)
                {
                    if ((DateTime.Now - _lastRefresh) > RefreshRate)
                    {
                        _lastRefresh = DateTime.Now;
                        Reset();
                    }
                }
            }
        }
    }
}