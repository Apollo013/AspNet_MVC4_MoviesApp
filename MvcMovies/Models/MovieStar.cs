using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovies.Models
{
    public class MovieStar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieStarId { get; set; }

        public List<Movie> StarMovies { get; set; }

        [Required(ErrorMessage = "Please specify a Name For The Movie Star")]
        public string name { get; set; }

        public MovieStar()
        {
            StarMovies = new List<Movie>();
        }
    }
}
