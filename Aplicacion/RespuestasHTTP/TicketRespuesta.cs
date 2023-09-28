
namespace Aplicacion.RespuestasHTTP
{
    public class TicketRespuesta
    {
        public List<Guid> tickets { get; set; }
        public FuncionRespuesta Funciones { get; set; }
        public string usuario { get; set; }
    }
}
