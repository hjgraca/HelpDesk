namespace HelpDesk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avaria",
                c => new
                    {
                        AvariaId = c.Int(nullable: false, identity: true),
                        Designacao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.AvariaId);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        DepartamentoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.DepartamentoId);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        EstadoId = c.Int(nullable: false, identity: true),
                        Designacao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.EstadoId);
            
            CreateTable(
                "dbo.Prioridade",
                c => new
                    {
                        PrioridadeId = c.Int(nullable: false, identity: true),
                        Designacao = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.PrioridadeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 4000),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 4000),
                        RoleId = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 4000),
                        Utilizador = c.String(maxLength: 4000),
                        Descricao = c.String(maxLength: 4000),
                        DataInsercao = c.DateTime(nullable: false),
                        DataConclusao = c.DateTime(),
                        EstadoId = c.Int(nullable: false),
                        AvariaId = c.Int(nullable: false),
                        TecnicoId = c.String(maxLength: 4000),
                        PrioridadeId = c.Int(nullable: false),
                        DepartamentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Avaria", t => t.AvariaId, cascadeDelete: true)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoId, cascadeDelete: true)
                .ForeignKey("dbo.Estado", t => t.EstadoId, cascadeDelete: true)
                .ForeignKey("dbo.Prioridade", t => t.PrioridadeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TecnicoId)
                .Index(t => t.EstadoId)
                .Index(t => t.AvariaId)
                .Index(t => t.TecnicoId)
                .Index(t => t.PrioridadeId)
                .Index(t => t.DepartamentoId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 4000),
                        Nome = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 4000),
                        SecurityStamp = c.String(maxLength: 4000),
                        PhoneNumber = c.String(maxLength: 4000),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Departamento_DepartamentoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.Departamento_DepartamentoId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Departamento_DepartamentoId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 4000),
                        ClaimType = c.String(maxLength: 4000),
                        ClaimValue = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 4000),
                        ProviderKey = c.String(nullable: false, maxLength: 4000),
                        UserId = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "TecnicoId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Departamento_DepartamentoId", "dbo.Departamento");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ticket", "PrioridadeId", "dbo.Prioridade");
            DropForeignKey("dbo.Ticket", "EstadoId", "dbo.Estado");
            DropForeignKey("dbo.Ticket", "DepartamentoId", "dbo.Departamento");
            DropForeignKey("dbo.Ticket", "AvariaId", "dbo.Avaria");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Departamento_DepartamentoId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ticket", new[] { "DepartamentoId" });
            DropIndex("dbo.Ticket", new[] { "PrioridadeId" });
            DropIndex("dbo.Ticket", new[] { "TecnicoId" });
            DropIndex("dbo.Ticket", new[] { "AvariaId" });
            DropIndex("dbo.Ticket", new[] { "EstadoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ticket");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Prioridade");
            DropTable("dbo.Estado");
            DropTable("dbo.Departamento");
            DropTable("dbo.Avaria");
        }
    }
}
