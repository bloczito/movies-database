using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }


        public Movie(int id, string title, string? description)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
        }
    }
}
