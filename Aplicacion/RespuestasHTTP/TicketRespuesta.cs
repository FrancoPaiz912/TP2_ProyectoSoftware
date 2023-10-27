
namespace Aplicacion.RespuestasHTTP
{
    public class TicketRespuesta
    {
        public List<CodigoTicketResponse> tickets { get; set; }
        public FuncionRespuesta Funcion { get; set; }
        public string usuario { get; set; }
    }
}
