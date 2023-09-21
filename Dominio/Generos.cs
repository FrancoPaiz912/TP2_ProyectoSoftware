namespace Dominio
{
    public class Generos
    {
        public int GenerosId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Peliculas> Peliculas { get; set; } 
    }
}
