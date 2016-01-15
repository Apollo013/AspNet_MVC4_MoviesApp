namespace MvcMovies.Migrations_MovieDBContext
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMovies.Contexts.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations_MovieDBContext";
        }

        protected override void Seed(MvcMovies.Contexts.MovieDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if(context.Movies.Count() == 0)
            {
                AddMovies(context);
                context.SaveChanges();
            }

            if(context.MovieStarz.Count() == 0)
            {
                AddMovieStars(context);
                context.SaveChanges();
            }


        }

        private void AddMovies(Contexts.MovieDBContext context)
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-1-11"),
                    Genre = "Romantic Comedy",
                    Rating = "G",
                    Price = 7.99M
                },

                 new Movie
                 {
                     Title = "Ghostbusters ",
                     ReleaseDate = DateTime.Parse("1984-3-13"),
                     Genre = "Comedy",
                     Rating = "G",
                     Price = 8.99M
                 },

                 new Movie
                 {
                     Title = "Ghostbusters 2",
                     ReleaseDate = DateTime.Parse("1986-2-23"),
                     Genre = "Comedy",
                     Rating = "G",
                     Price = 9.99M
                 },

               new Movie
               {
                   Title = "Rio Grande",
                   ReleaseDate = DateTime.Parse("1959-4-15"),
                   Genre = "Western",
                   Rating = "G",
                   Price = 3.99M
               },

               new Movie
               {
                   Title = "Batman The Dark Knight Rises",
                   ReleaseDate = DateTime.Parse("2012-4-15"),
                   Genre = "Action",
                   Rating = "PG",
                   Price = 11.99M
               },

               new Movie
               {
                   Title = "Batman The Dark Knight",
                   ReleaseDate = DateTime.Parse("2008-4-15"),
                   Genre = "Action",
                   Rating = "PG",
                   Price = 11.99M
               },

               new Movie
               {
                   Title = "Batman Begins",
                   ReleaseDate = DateTime.Parse("2005-4-15"),
                   Genre = "Action",
                   Rating = "PG",
                   Price = 11.99M
               },

               new Movie
               {
                   Title = "Once Upon A Time In The West",
                   ReleaseDate = DateTime.Parse("1969-4-15"),
                   Genre = "Western",
                   Rating = "PG",
                   Price = 9.99M
               }
            };

            context.Movies.AddRange(movies);

        }

        private void AddMovieStars(Contexts.MovieDBContext context)
        {

            var moviesq = from m in context.Movies select(m);

            List<Movie> movies = moviesq.ToList();

            List<MovieStar> stars = new List<MovieStar>()
            {
                new MovieStar {name = "Billy Crystal" , StarMovies = new List<Movie>() { movies[0] } },
                new MovieStar {name = "Meg Ryan" , StarMovies = new List<Movie>() { movies[0] }  },
                new MovieStar {name = "Bill Murray" , StarMovies = new List<Movie>() { movies[1], movies[2] }  },
                new MovieStar {name = "Dan Ackroyd" , StarMovies = new List<Movie>() { movies[1], movies[2] }  },
                new MovieStar {name = "John Wayne"  , StarMovies = new List<Movie>() { movies[3] } },
                new MovieStar {name = "Maureen OHara" , StarMovies = new List<Movie>() { movies[3] }  },
                new MovieStar {name = "Christian Bale" , StarMovies = new List<Movie>() { movies[4], movies[5], movies[6] }  },
                new MovieStar {name = "Claudia Cardinale" , StarMovies = new List<Movie>() { movies[7] }  },
                new MovieStar {name = "Henry Fonda" , StarMovies = new List<Movie>() { movies[7] }  },
                new MovieStar {name = "Charles Bronson" , StarMovies = new List<Movie>() { movies[7] }  }
            };

            context.MovieStarz.AddRange(stars);
        }
    }


}
