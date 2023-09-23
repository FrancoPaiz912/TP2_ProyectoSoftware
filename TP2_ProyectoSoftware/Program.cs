using Aplicacion;
using Aplicacion.Casos_de_usos;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Aplicacion.Interfaces.Infraestructura;
using Infraestructura.Consultas_DB;
using Infraestructura.CUD_DB;
using Infraestructura.EstructuraDB;
using Infraestructura.Inserts;
using Infraestructura.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
builder.Services.AddDbContext<Contexto_Cine>(options =>
{
    options.UseSqlServer("Server=localhost;Database=Contexto_Cine;Trusted_Connection=True;TrustServerCertificate=True;Persist Security Info=true");

});

builder.Services.AddTransient<IAgregar,Insertar_Funcion>();
builder.Services.AddTransient<IConsultasFunciones, Consulta_Funcion>();
builder.Services.AddTransient<IServiciosFunciones, ServiciosFunciones>();
builder.Services.AddTransient<IEliminar, Eliminar_Funcion>();
builder.Services.AddTransient<IServiciosSalas, ServiciosSalas>();
builder.Services.AddTransient<IConsultasSalas, Consultas_Salas>();
builder.Services.AddTransient<IServiciosPeliculas, ServiciosPeliculas>();
builder.Services.AddTransient<IActualizarPeliculas, Actualizar_Pelicula>();
builder.Services.AddTransient<IConsultasPeliculas, Consultas_Peliculas>();

var app = builder.Build(); 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
