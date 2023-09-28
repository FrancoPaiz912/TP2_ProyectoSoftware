
namespace Aplicacion.RespuestasHTTP
{
    public class TicketRespuesta
    {
        public List<Guid> tickets { get; set; }
        public FuncionCompletaRespuesta Funciones { get; set; }
        public string usuario { get; set; }
    }
}
