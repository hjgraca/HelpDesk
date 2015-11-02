using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDesk.Data.Entitidades
{
    public class TicketContext : IdentityDbContext<ApplicationUser>
    {
        public TicketContext()
            : base("TicketContext", throwIfV1Schema: false)
        {
        }

        //private static DbConnection ConnectionString()
        //{
        //    var conn = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0").CreateConnection();
        //    conn.ConnectionString = @"Data Source=..\HelpDesk\App_Data\HelpDesk.sdf";
        //    throw new Exception(Directory.GetParent(@"..\HelpDesk").Name);


        //    return conn;
        //    Path.Combine(@"..\HelpDesk\App_Data\HelpDesk.sdf");
        //    //return @"Data Source=|DataDirectory|\\HelpDesk.sdf";
        //    throw new Exception("");

        //}

        public static TicketContext Create()
        {
            return new TicketContext();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        //public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Avaria> Avarias { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
