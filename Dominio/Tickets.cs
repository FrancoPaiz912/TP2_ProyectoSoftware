namespace Dominio
{
    public class Tickets
    {
        public int TicketsId { get; set; }
        public int FuncionId { get; set; } 
        public string Usuario { get; set; }
        public Funciones Funciones { get; set; }
    }
}
