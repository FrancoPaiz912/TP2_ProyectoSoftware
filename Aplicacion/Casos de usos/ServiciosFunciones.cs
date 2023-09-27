using Aplicacion.DTO;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;
using Dominio;
using System;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetFuncionesRespuesta()
        {
            List<Funciones> Funciones = await _Consultas.ListarFunciones();
            List<FuncionCompletaRespuesta> result= new List<FuncionCompletaRespuesta> ();
            foreach (var item in Funciones)
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

        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetFuncionesDia(DateTime dia, List<FuncionCompletaRespuesta> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (result.Count() == 0 && dia != null)
            {
                result = await _Consultas.ListarFecha(dia, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new FuncionCompletaRespuesta
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
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.Fecha == dia).ToList();  
                return funciones;
            }
        }

        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetFuncionesNombrePelicula(string? Pelicula, List<FuncionCompletaRespuesta> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (funciones.Count() == 0 && Pelicula != null)
            {
                result = await _Consultas.ListarPeliculas(Pelicula, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new FuncionCompletaRespuesta
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
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.Pelicula.Titulo.Contains(Pelicula)).ToList();  
                return funciones;
            }
        }

        async Task<List<FuncionCompletaRespuesta>> IServiciosFunciones.GetFuncionesGenero(int? GeneroID, List<FuncionCompletaRespuesta> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (funciones.Count() == 0 && GeneroID != null)
            {
                result = await _Consultas.ListarGeneros(GeneroID, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new FuncionCompletaRespuesta
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
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.Pelicula.Genero.Id == GeneroID).ToList();
                return funciones;
            }
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
            return await _Consultas.GetIDs(IdPelicula,IdSala);
        }

        async Task<Funciones> IServiciosFunciones.ComprobarFunciones(int id)
        {
            return await _Consultas.GetIdFuncion(id);
        }

        async Task<EliminarFuncionResponse> IServiciosFunciones.EliminarFuncion(Funciones funcion)
        {
            if (await _Eliminar.RemoverFuncion(funcion))
            {
                return new EliminarFuncionResponse
                {
                    FuncionId = funcion.FuncionesId,
                    Fecha = funcion.Fecha,
                    Horario = funcion.Hora,
                };
            }
            else return null;
        }

        async Task<bool> IServiciosFunciones.ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora)
        {
            return await _Consultas.ComprobacionHoraria(Salaid, Fecha, Hora);
        }

        async Task<TicketRespuesta> IServiciosFunciones.GenerarTicket(int ID, TicketDTO ticket)
        {
            Tickets Response= new Tickets();
            List<Guid> ListaTickets = new List<Guid>();
            //Crear ticket y devolver datos de respuesta.
            for (int i = 0; i < ticket.Cantidad; i++)
            {
                Response = await _Agregar.AgregarTicket(new Tickets
                {
                    FuncionId = ID,
                    Usuario = ticket.Usuario,
                });
                ListaTickets.Add(Response.TicketsId); //Ver porque almacena siempre el mismo código de ticket.
            }

            return new TicketRespuesta
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
