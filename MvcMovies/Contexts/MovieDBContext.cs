using MvcMovies.Models;
using System.Data.Entity;

namespace MvcMovies.Contexts
{
    public class MovieDBContext : DbContext
    {
        public MovieDBContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<MovieStar> MovieStarz { get; set; }
        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieStar>().ToTable("MovieStars", "Movies");
            modelBuilder.Entity<Movie>().ToTable("Movies", "Movies");

            modelBuilder.Entity<Movie>().HasMany(m => m.MovieStars)
                .WithMany(ms => ms.StarMovies)
                .Map(
                x => 
                {
                    x.MapLeftKey("MovieId");
                    x.MapRightKey("MovieStarId");
                    x.ToTable("StarMovies", "Movies");
                });

        }

    }
}
