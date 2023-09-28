
namespace Aplicacion.RespuestasHTTP
{
    public class PeliculaCompletaResponse
    {
        public int Peliculaid { get; set; }
        public string Titulo { get; set; }
        public string Poster { get; set; }
        public string Sinopsis { get; set; }
        public string Trailer { get; set; }
        public GeneroRespuesta Genero { get; set; }
        public List<FuncionRespuesta> funciones { get; set; }
    }
}
