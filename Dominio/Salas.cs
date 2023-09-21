namespace Dominio
{
    public class Salas
    {
        public int SalasId { get; set; } 
        public string Nombre { get; set; } 
        public int Capacidad { get; set; } 
        public ICollection<Funciones> Funciones { get; set; }
    }
}
