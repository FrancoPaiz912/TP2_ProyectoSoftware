
namespace Aplicacion.RespuestasHTTP
{
    public class FuncionRespuesta
    {
        public int FuncionId { get; set; }
        public InfoPeliculasParaFuncionesRespuesta Pelicula { get; set; }
        public SalaRespuesta Sala { get; set; }
        public DateTime Fecha { get; set; }
        public string Horario { get; set; }
    }
}
