
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace BookStore23011093.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BookStore23011093.Data.BookStore23011093Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}