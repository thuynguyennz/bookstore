using BookStore23011093.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BookStore23011093.ViewModels
{
    public class BookIndexViewModel
    {
        public IPagedList<Book> Books { get; set; }
        public string Search { get; set; }
        public IEnumerable<GenreWithCount> GenresWithCount { get; set; }
        public string Genre { get; set; }
        public IEnumerable<SelectListItem> GenreFilterItems
        {
            get
            {
                var allGenres = GenresWithCount.Select(cc => new SelectListItem
                {
                    Value = cc.GenreName,
                    Text = cc.GenreNameWithCount
                });
                return allGenres;
            }
        }
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        
        public class GenreWithCount
        {
            public int BookCount { get; set; }
            public string GenreName { get; set; }
            public string GenreNameWithCount
            {
                get
                {
                    return GenreName + " (" + BookCount.ToString() + ")";
                }
            }
        }
    }
}
