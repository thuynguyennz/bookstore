using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore23011093.Models
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Genre")]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}