using System;

namespace HelpDesk.Data.Entitidades
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Titulo { get; set; }
        public string Utilizador { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInsercao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
        public int AvariaId { get; set; }
        public virtual Avaria Avaria { get; set; }
        public string TecnicoId { get; set; }
        public virtual ApplicationUser Tecnico { get; set; }
        public int PrioridadeId { get; set; }
        public virtual Prioridade Prioridade { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
