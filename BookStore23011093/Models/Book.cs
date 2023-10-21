using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore23011093.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required,
         Display(Name = "Book Title")]
        public string Title { get; set; }

        [MaxLength(length: 100)]
        public string Description { get; set; }
        public string Language { get; set; }

        [Required,
         MaxLength(length: 13)]
        public string ISBN { get; set; }

        [Required,
         DataType(DataType.Date),
         Display(Name = "Date Published")]
        public DateTime DatePublished { get; set; }

        [Required,
         DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Author { get; set; }

         public int? GenreID { get; set; }
        public virtual Genre Genre { get; set; }

    }
}