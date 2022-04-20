namespace BetSmart.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bet",
                c => new
                    {
                        BetId = c.Int(nullable: false, identity: true),
                        SportsBookId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Event = c.String(),
                        Stake = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Odds = c.Int(nullable: false),
                        DidWin = c.Boolean(nullable: false),
                        SportsBook_SportsBookId = c.Int(),
                        SportsBook_SportsBookId1 = c.Int(),
                        SportsBook_SportsBookId2 = c.Int(),
                    })
                .PrimaryKey(t => t.BetId)
                .ForeignKey("dbo.SportsBook", t => t.SportsBook_SportsBookId)
                .ForeignKey("dbo.SportsBook", t => t.SportsBook_SportsBookId1)
                .ForeignKey("dbo.SportsBook", t => t.SportsBook_SportsBookId2)
                .ForeignKey("dbo.SportsBook", t => t.SportsBookId, cascadeDelete: true)
                .Index(t => t.SportsBookId)
                .Index(t => t.SportsBook_SportsBookId)
                .Index(t => t.SportsBook_SportsBookId1)
                .Index(t => t.SportsBook_SportsBookId2);
            
            CreateTable(
                "dbo.SportsBook",
                c => new
                    {
                        SportsBookId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(),
                        WinLossRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SportsBookId);
            
            CreateTable(
                "dbo.Overview",
                c => new
                    {
                        OverallId = c.Int(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WinLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProfitLoss = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OverallId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Bet", "SportsBookId", "dbo.SportsBook");
            DropForeignKey("dbo.Bet", "SportsBook_SportsBookId2", "dbo.SportsBook");
            DropForeignKey("dbo.Bet", "SportsBook_SportsBookId1", "dbo.SportsBook");
            DropForeignKey("dbo.Bet", "SportsBook_SportsBookId", "dbo.SportsBook");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Bet", new[] { "SportsBook_SportsBookId2" });
            DropIndex("dbo.Bet", new[] { "SportsBook_SportsBookId1" });
            DropIndex("dbo.Bet", new[] { "SportsBook_SportsBookId" });
            DropIndex("dbo.Bet", new[] { "SportsBookId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Overview");
            DropTable("dbo.SportsBook");
            DropTable("dbo.Bet");
        }
    }
}
