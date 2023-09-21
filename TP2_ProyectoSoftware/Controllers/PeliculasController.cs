using Aplicacion.Interfaces.Aplicacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IServiciosFunciones _Servicio;

        public PeliculasController(IServiciosFunciones Servicio)
        {
            _Servicio = Servicio;
        }
    }
}
