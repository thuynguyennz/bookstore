using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore23011093.Data
{
    public class BookStore23011093Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BookStore23011093Context>(null);
            base.OnModelCreating(modelBuilder);
        }
        public BookStore23011093Context() : base("name=BookStore23011093Context")
        {
        }

        public System.Data.Entity.DbSet<BookStore23011093.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<BookStore23011093.Models.Genre> Genres { get; set; }
    }
}
