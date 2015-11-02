using HelpDesk.Data.Entitidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace HelpDesk.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HelpDesk.Data.Entitidades.TicketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HelpDesk.Data.Entitidades.TicketContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TicketContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TicketContext()));

            var user = new ApplicationUser()
            {
                UserName = "hreview@mail.com",
                Email = "hreview@mail.com",
                EmailConfirmed = true,
                Nome = "Taiseer"
            };
            
            manager.Create(user, "123456");

            var departamento = new Departamento
            {
                DepartamentoId = 1,
                Nome = "Dep1"
            };

            context.Departamentos.Add(departamento);

            user.Departamento = departamento;

            context.Tickets.Add(new Ticket
            {
                Departamento = departamento,
                TecnicoId = user.Id,
                TicketId = 1,
                Descricao = "Test",
                Avaria = new Avaria
                {
                    AvariaId = 1,
                    Designacao = "test"
                },
                Estado = new Estado
                {
                    EstadoId = 1,
                    Designacao = "fdf"
                },
                Prioridade = new Prioridade
                {
                    PrioridadeId = 1,
                    Designacao = "pr"
                },
                DataInsercao = DateTime.Now,
                Utilizador = "Test1"
            });


            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Tecnico" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("hreview@mail.com");

            manager.AddToRoles(adminUser.Id, "Admin");
        }
    }
}
