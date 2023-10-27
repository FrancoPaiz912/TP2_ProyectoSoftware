namespace Dominio
{
    public class Salas
    {
        public int SalaId { get; set; } 
        public string Nombre { get; set; } 
        public int Capacidad { get; set; } 
        public ICollection<Funciones> Funciones { get; set; }
    }
}
