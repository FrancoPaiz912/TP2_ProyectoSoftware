﻿
namespace Aplicacion.RespuestasHTTP
{
    public class InfoPeliculasParaFuncionesRespuesta
    {
        public int Peliculaid { get; set; }
        public string Titulo { get; set; }
        public string Poster { get; set; }
        public GeneroRespuesta Genero { get; set; }
    }
}