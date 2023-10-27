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
        async Task<List<FuncionRespuesta>> IServiciosFunciones.GetFuncionesRespuesta(string fecha, string titulo, int? Genero)
        {
            List<Funciones> Funciones = await _Consultas.ListarFunciones(fecha, titulo, Genero); //Llamo a infraestructura
            List<FuncionRespuesta> result = new List<FuncionRespuesta>();
            foreach (var item in Funciones) //Si se tienen resultados, armo el response correspondiente y lo devuelvo.
            {
                result.Add(new FuncionRespuesta
                {
                    FuncionId = item.FuncionId,
                    Pelicula = new InfoPeliculasParaFuncionesRespuesta
                    {
                        PeliculaId = item.PeliculaId,
                        Titulo = item.Peliculas.Titulo,
                        Poster = item.Peliculas.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = item.Peliculas.Generos.GeneroId,
                            Nombre = item.Peliculas.Generos.Nombre
                        },
                    },
                    Sala = new SalaRespuesta
                    {
                        Id = item.Salas.SalaId,
                        Nombre = item.Salas.Nombre,
                        Capacidad = item.Salas.Capacidad,
                    },
                    Fecha = item.Fecha,
                    Horario = item.Hora,
                });
            }
            return result;
        }

        async Task<List<FuncionRespuesta>> IServiciosFunciones.GetCartelera(List<FuncionRespuesta> funciones)
        {
            List<FuncionRespuesta> cartelera = new List<FuncionRespuesta>();
            foreach (var item in funciones)
            {
                   cartelera.Add(new FuncionRespuesta
                {
                    FuncionId = item.FuncionId,
                    Pelicula = new InfoPeliculasParaFuncionesRespuesta
                    {
                        PeliculaId = item.Pelicula.PeliculaId,
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

        async Task<FuncionRespuesta> IServiciosFunciones.AddFunciones(FuncionesDTO funcion)
        {
            await _Agregar.AgregarFuncion(new Funciones
            {
                PeliculaId = funcion.pelicula,
                SalaId = funcion.sala,
                Fecha = funcion.fecha,
                Hora = DateTime.Parse(funcion.horario).TimeOfDay,
            });

            Funciones func= await _Consultas.RetornarRegistro();
            return new FuncionRespuesta
            {
                FuncionId = func.FuncionId,
                Pelicula = new InfoPeliculasParaFuncionesRespuesta
                {
                    PeliculaId = func.PeliculaId,
                    Titulo = func.Peliculas.Titulo,
                    Poster = func.Peliculas.Poster,
                    Genero = new GeneroRespuesta
                    {
                        Id = func.Peliculas.Generos.GeneroId,
                        Nombre = func.Peliculas.Generos.Nombre
                    },
                },
                Sala = new SalaRespuesta
                {
                    Id = func.Salas.SalaId,
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
        async Task<FuncionEliminadaResponse> IServiciosFunciones.EliminarFuncion(Funciones funcion)
        {
            if (await _Eliminar.RemoverFuncion(funcion)) //Enviamos la funcion a infraestructura para que la elimine de la BD y en caso de poder eliminar la función preparamos el response 
            {
                return new FuncionEliminadaResponse //Con los datos de función se arma el response de la función eliminada.
                {
                    FuncionId = funcion.FuncionId,
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
            Funciones Response = new Funciones();
            List<CodigoTicketResponse> ListaTickets = new List<CodigoTicketResponse>();
            for (int i = 0; i < ticket.Cantidad; i++)
            {//Agregamos los tickets solicitados a la base de datos
                Response = await _Agregar.AgregarTicket(new Tickets
                {
                    FuncionId = ID,
                    Usuario = ticket.Usuario,
                });
                ListaTickets.Add(new CodigoTicketResponse {
                    TicketId = Response.Tickets.ElementAt(Response.Tickets.Count - 1).TicketId,
                });
            }

            //int ignorar = -1; //Tiene que entrar en la primera, en la segunda no, y luego si.
            //if (ticket.Cantidad > 0)
            //{
                //foreach (var item in Response.Funciones.Tickets) //No sé porque siempre en la posición 1 del arreglo, se agrega un id viejardo
                //{
                //    if (item.Usuario == ticket.Usuario)
                //    {
                //        //if (ignorar != 0)
                //        //{
                //            ListaTickets.Add(item.TicketId);
                //        //}
                //    };
                //    ignorar++;
                //}

                return new TicketRespuesta //Creamos y devolvemos la respuesta 
                {
                    tickets = ListaTickets,
                    Funcion = new FuncionRespuesta
                    {
                        FuncionId = Response.PeliculaId,
                        Pelicula = new InfoPeliculasParaFuncionesRespuesta
                        {
                            PeliculaId = Response.PeliculaId,
                            Titulo = Response.Peliculas.Titulo,
                            Poster = Response.Peliculas.Poster,
                            Genero = new GeneroRespuesta
                            {
                                Id = Response.Peliculas.Generos.GeneroId,
                                Nombre = Response.Peliculas.Generos.Nombre
                            },
                        },
                        Sala = new SalaRespuesta
                        {
                            Id = Response.Salas.SalaId,
                            Nombre = Response.Salas.Nombre,
                            Capacidad = Response.Salas.Capacidad,
                        },
                        Fecha = Response.Fecha,
                        Horario = Response.Hora,
                    },
                    usuario = Response.Tickets.ElementAt(Response.Tickets.Count - 1).Usuario,
                };
            }
        //}

        async Task<FuncionRespuesta> IServiciosFunciones.GetDatosFuncion(int id)
        {
            Funciones funcion = await _Consultas.GetIdFuncion(id);
            if (funcion != null)
            {
                return new FuncionRespuesta
                {
                    FuncionId = funcion.FuncionId,
                    Pelicula = new InfoPeliculasParaFuncionesRespuesta
                    {
                        PeliculaId = funcion.PeliculaId,
                        Titulo = funcion.Peliculas.Titulo,
                        Poster = funcion.Peliculas.Poster,
                        Genero = new GeneroRespuesta
                        {
                            Id = funcion.Peliculas.Generos.GeneroId,
                            Nombre = funcion.Peliculas.Generos.Nombre
                        },
                    },
                    Sala = new SalaRespuesta{
                        Id = funcion.Salas.SalaId,
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
