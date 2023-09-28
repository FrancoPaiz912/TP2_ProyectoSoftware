using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;
using Dominio;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosFunciones : IServiciosFunciones
    {
        private readonly IConsultasFunciones _Consultas;
        private readonly IAgregar _Agregar;
        private readonly IEliminar _Eliminar;
        public ServiciosFunciones(IConsultasFunciones consultas, IAgregar Agregar, IEliminar Eliminar)
        {
            _Consultas = consultas;
            _Agregar = Agregar;
            _Eliminar = Eliminar;
        }

        //Gets
        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetFuncionesRespuesta(string fecha, string titulo, int? Genero)
        {
            List<Funciones> Funciones = await _Consultas.ListarFunciones(fecha, titulo, Genero); //Llamo a infraestructura
            List<FuncionCompletaRespuesta> result = new List<FuncionCompletaRespuesta>();
            foreach (var item in Funciones) //Si se tienen resultados, armo el response correspondiente y lo devuelvo.
            {
                result.Add(new FuncionCompletaRespuesta
                {
                    FuncionId = item.FuncionesId,
                    Pelicula = new PeliculaRespuesta
                    {
                        Peliculaid = item.PeliculaId,
                        Titulo = item.Peliculas.Titulo,
                        Poster = item.Peliculas.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = item.Peliculas.Generos.GenerosId,
                            Nombre = item.Peliculas.Generos.Nombre
                        },
                    },
                    Sala = new SalaRespuesta
                    {
                        Id = item.Salas.SalasId,
                        Nombre = item.Salas.Nombre,
                        Capacidad = item.Salas.Capacidad,
                    },
                    Fecha = item.Fecha,
                    Horario = item.Hora,
                });
            }
            return result;
        }

        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetCartelera(List<FuncionCompletaRespuesta> funciones)
        {
            List<FuncionCompletaRespuesta> cartelera = new List<FuncionCompletaRespuesta>();
            foreach (var item in funciones)
            {
                   cartelera.Add(new FuncionCompletaRespuesta
                {
                    FuncionId = item.FuncionId,
                    Pelicula = new PeliculaRespuesta
                    {
                        Peliculaid = item.Pelicula.Peliculaid,
                        Titulo = item.Pelicula.Titulo,
                        Poster = item.Pelicula.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = item.Pelicula.Genero.Id,
                            Nombre = item.Pelicula.Genero.Nombre
                        },
                    },
                    Sala = new SalaRespuesta
                    {
                        Id = item.Sala.Id,
                        Nombre = item.Sala.Nombre,
                        Capacidad = item.Sala.Capacidad,
                    },
                    Fecha = item.Fecha,
                    Horario = item.Horario,
                });
            }
            return cartelera;
        }

        async Task<FuncionCompletaRespuesta> IServiciosFunciones.AddFunciones(FuncionesDTO funcion)
        {
            await _Agregar.AgregarFuncion(new Funciones
            {
                PeliculaId = funcion.PeliculaId,
                SalaId = funcion.SalaId,
                Fecha = funcion.Fecha,
                Hora = DateTime.Parse(funcion.Hora).TimeOfDay,
            });

            Funciones func= await _Consultas.RetornarRegistro();
            return new FuncionCompletaRespuesta
            {
                FuncionId = func.FuncionesId,
                Pelicula = new PeliculaRespuesta
                {
                    Peliculaid = func.PeliculaId,
                    Titulo = func.Peliculas.Titulo,
                    Poster = func.Peliculas.Poster,
                    Genero = new GeneroRespuesta
                    {
                        Id = func.Peliculas.Generos.GenerosId,
                        Nombre = func.Peliculas.Generos.Nombre
                    },
                },
                Sala = new SalaRespuesta
                {
                    Id = func.Salas.SalasId,
                    Nombre = func.Salas.Nombre,
                    Capacidad = func.Salas.Capacidad,
                },
                Fecha = func.Fecha,
                Horario = func.Hora,
            };
        }

        async Task<List<bool>> IServiciosFunciones.GetId(int IdPelicula,int IdSala)
        {
            return await _Consultas.GetIDs(IdPelicula,IdSala); //Retorno una lista con dos boolean que representan los ID de pelicula y sala. 
        }

        async Task<Funciones> IServiciosFunciones.ComprobarFunciones(int id)
        {
            return await _Consultas.GetIdFuncion(id); //Comprobamos que exista la función y la devolvemos si existe
        }
        async Task<EliminarFuncionResponse> IServiciosFunciones.EliminarFuncion(Funciones funcion)
        {
            if (await _Eliminar.RemoverFuncion(funcion)) //Enviamos la funcion a infraestructura para que la elimine de la BD y en caso de poder eliminar la función preparamos el response 
            {
                return new EliminarFuncionResponse //Con los datos de función se arma el response de la función eliminada.
                {
                    FuncionId = funcion.FuncionesId,
                    Fecha = funcion.Fecha,
                    Horario = funcion.Hora,
                };
            }
            else return null; //Si no se pudo eliminar la función se retorna un null que servirá para indicar justamente esto.
        }

        async Task<bool> IServiciosFunciones.ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora)
        {
            return await _Consultas.ComprobacionHoraria(Salaid, Fecha, Hora); //Llamo a infraestructura y devuelvo un bool según este disponible o no el horario.
        }

        async Task<TicketRespuesta> IServiciosFunciones.GenerarTicket(int ID, TicketDTO ticket)
        {
            Tickets Response= new Tickets(); 
            List<Guid> ListaTickets = new List<Guid>();
            for (int i = 0; i < ticket.Cantidad; i++)
            {//Agregamos los tickets solicitados a la base de datos
                Response = await _Agregar.AgregarTicket(new Tickets
                {
                    FuncionId = ID,
                    Usuario = ticket.Usuario,
                });
            }

            int ignorar = -1; //Tiene que entrar en la primera, en la segunda no, y luego si.

            foreach (var item in Response.Funciones.Tickets) //No sé porque siempre en la posición 1 del arreglo, se agrega un id viejardo
            {
                if (item.Usuario == ticket.Usuario)
                {
                    if (ignorar != 0)
                    {
                        ListaTickets.Add(item.TicketsId);
                    }
                };
                ignorar++;
            }

            return new TicketRespuesta //Creamos y devolvemos la respuesta 
            {
                tickets = ListaTickets,
                Funciones = new FuncionCompletaRespuesta
                {
                    FuncionId = Response.Funciones.PeliculaId,
                    Pelicula = new PeliculaRespuesta
                    {
                        Peliculaid = Response.Funciones.PeliculaId,
                        Titulo = Response.Funciones.Peliculas.Titulo,
                        Poster = Response.Funciones.Peliculas.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = Response.Funciones.Peliculas.Generos.GenerosId,
                            Nombre = Response.Funciones.Peliculas.Generos.Nombre
                        },
                    },
                    Sala = new SalaRespuesta
                    {
                        Id = Response.Funciones.Salas.SalasId,
                        Nombre = Response.Funciones.Salas.Nombre,
                        Capacidad = Response.Funciones.Salas.Capacidad,
                    },
                    Fecha = Response.Funciones.Fecha,
                    Horario = Response.Funciones.Hora,
                },
                usuario = Response.Usuario,
            };
        }

        async Task<FuncionCompletaRespuesta> IServiciosFunciones.GetDatosFuncion(int id)
        {
            Funciones funcion = await _Consultas.GetIdFuncion(id);
            if (funcion != null)
            {
                return new FuncionCompletaRespuesta
                {
                    FuncionId = funcion.FuncionesId,
                    Pelicula = new PeliculaRespuesta
                    {
                        Peliculaid = funcion.PeliculaId,
                        Titulo = funcion.Peliculas.Titulo,
                        Poster = funcion.Peliculas.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = funcion.Peliculas.Generos.GenerosId,
                            Nombre = funcion.Peliculas.Generos.Nombre
                        },
                    },
                    Sala = new SalaRespuesta{
                        Id = funcion.Salas.SalasId,
                        Nombre = funcion.Salas.Nombre,
                        Capacidad = funcion.Salas.Capacidad,
                    },
                    Fecha = funcion.Fecha,
                    Horario = funcion.Hora,
                };
            }
            return null; 
        }

    }
}
