namespace MvcMovies.Migrations_ApplicationDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Movies.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Movies.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Movies.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Movies.Users", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "Movies.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Movies.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Movies.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "Movies.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Movies.Users", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "Movies.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Movies.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Movies.AspNetUsers", "Id", "Movies.Users");
            DropForeignKey("Movies.UserRoles", "IdentityUser_Id", "Movies.Users");
            DropForeignKey("Movies.UserLogins", "IdentityUser_Id", "Movies.Users");
            DropForeignKey("Movies.UserClaims", "IdentityUser_Id", "Movies.Users");
            DropForeignKey("Movies.UserRoles", "RoleId", "Movies.Roles");
            DropIndex("Movies.AspNetUsers", new[] { "Id" });
            DropIndex("Movies.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("Movies.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("Movies.Users", "UserNameIndex");
            DropIndex("Movies.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("Movies.UserRoles", new[] { "RoleId" });
            DropIndex("Movies.Roles", "RoleNameIndex");
            DropTable("Movies.AspNetUsers");
            DropTable("Movies.UserLogins");
            DropTable("Movies.UserClaims");
            DropTable("Movies.Users");
            DropTable("Movies.UserRoles");
            DropTable("Movies.Roles");
        }
    }
}
