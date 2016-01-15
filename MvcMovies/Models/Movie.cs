using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovies.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }


        [Required(ErrorMessage = "Please specify a Title")]
        public string Title { get; set; }


        //[DisplayFormat(DataFormatString = "{0:d}")]
        //[DataType(DataType.Date)]

        [Required(ErrorMessage = "Please specify a Data")]
        [UIHint("LoudDateTime")]
        public DateTime ReleaseDate { get; set; }


        [Required(ErrorMessage = "Please specify a Genre")]
        public string Genre { get; set; }


        [Required(ErrorMessage = "Please specify a Price")]
        [Range(0.01, 100.00, ErrorMessage = "Please enter a value between 0.01 and 100.00")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Please specify a Rating")]
        [StringLength(5)]
        public string Rating { get; set; }

        public List<MovieStar> MovieStars { get; set; }

        public Movie()
        {
            MovieStars = new List<MovieStar>();
            ReleaseDate = new DateTime();
        }
    }
}
