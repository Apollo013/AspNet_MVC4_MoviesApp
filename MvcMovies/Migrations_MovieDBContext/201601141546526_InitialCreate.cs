namespace MvcMovies.Migrations_MovieDBContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Movies.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Genre = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "Movies.MovieStars",
                c => new
                    {
                        MovieStarId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MovieStarId);
            
            CreateTable(
                "Movies.StarMovies",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        MovieStarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.MovieStarId })
                .ForeignKey("Movies.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("Movies.MovieStars", t => t.MovieStarId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.MovieStarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Movies.StarMovies", "MovieStarId", "Movies.MovieStars");
            DropForeignKey("Movies.StarMovies", "MovieId", "Movies.Movies");
            DropIndex("Movies.StarMovies", new[] { "MovieStarId" });
            DropIndex("Movies.StarMovies", new[] { "MovieId" });
            DropTable("Movies.StarMovies");
            DropTable("Movies.MovieStars");
            DropTable("Movies.Movies");
        }
    }
}
