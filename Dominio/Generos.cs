namespace Dominio
{
    public class Generos
    {
        public int GeneroId { get; set; }
        public string Nombre { get; set; }
        public ICollection<Peliculas> Peliculas { get; set; }
    }
}
