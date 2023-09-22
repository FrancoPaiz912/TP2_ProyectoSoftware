using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.EstructuraDB
{
    public class Contexto_Cine : DbContext
    {
        public DbSet<Funciones> Funciones { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Peliculas> Peliculas { get; set; }
        public DbSet<Salas> Salas { get; set; }
        public DbSet<Tickets> Tickets { get; set; }

        public Contexto_Cine(DbContextOptions<Contexto_Cine> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Cine_Contexto;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Peliculas>(Entity =>
            {
                Entity.ToTable("Peliculas");
                Entity.HasKey(pk => pk.Peliculasid);
                Entity.Property(pk => pk.Peliculasid).ValueGeneratedOnAdd().IsRequired();
                Entity.Property(a => a.Titulo).HasMaxLength(50).IsRequired();
                Entity.Property(a => a.Sinopsis).HasMaxLength(255).IsRequired();
                Entity.Property(a => a.Poster).HasMaxLength(100).IsRequired();
                Entity.Property(a => a.Trailer).HasMaxLength(100).IsRequired();
                Entity.HasOne<Generos>(tp => tp.Generos)
                .WithMany(lt => lt.Peliculas)
                .HasForeignKey(fk => fk.Genero);
            });

            modelBuilder.Entity<Funciones>(Entity =>
            {
                Entity.ToTable("Funciones");
                Entity.HasKey(pk => pk.FuncionesId);
                Entity.Property(pk => pk.FuncionesId).ValueGeneratedOnAdd().IsRequired();
                Entity.Property(a => a.Hora).IsRequired();
                Entity.Property(a => a.Fecha).IsRequired();
                Entity.HasOne<Peliculas>(tp => tp.Peliculas)
                .WithMany(lt => lt.Funciones)
                .HasForeignKey(fk => fk.PeliculaId);
            });

            modelBuilder.Entity<Salas>(Entity =>
            {
                Entity.ToTable("Salas");
                Entity.HasKey(pk => pk.SalasId);
                Entity.Property(pk => pk.SalasId).ValueGeneratedOnAdd().IsRequired();
                Entity.Property(a => a.Nombre).HasMaxLength(50).IsRequired();
                Entity.Property(a => a.Capacidad).IsRequired();
                Entity.HasMany<Funciones>(lt => lt.Funciones)
                .WithOne(tp => tp.Salas)
                .HasForeignKey(fk => fk.SalaId);
            });

            modelBuilder.Entity<Tickets>(Entity =>
            {
                Entity.ToTable("Tickets");
                Entity.HasKey(pk => pk.TicketsId);
                Entity.Property(pk => pk.TicketsId).ValueGeneratedOnAdd().IsRequired();
                Entity.Property(a => a.Usuario).HasMaxLength(50).IsRequired();
                Entity.HasIndex(pk => pk.TicketsId).IsUnique();
                Entity.HasOne<Funciones>(tp => tp.Funciones)
                .WithMany(lt => lt.Tickets)
                .HasForeignKey(fk => fk.FuncionId);
            });

            modelBuilder.Entity<Generos>(Entity =>
            {
                Entity.ToTable("Generos");
                Entity.HasKey(pk => pk.GenerosId);
                Entity.Property(pk => pk.GenerosId).ValueGeneratedOnAdd().IsRequired();
                Entity.Property(a => a.Nombre).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Generos>(Entity =>
            {
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 1,
                    Nombre = "Acción",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 2,
                    Nombre = "Aventuras",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 3,
                    Nombre = "Ciencia Ficción",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 4,
                    Nombre = "Comedia",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 5,
                    Nombre = "Documental",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 6,
                    Nombre = "Drama",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 7,
                    Nombre = "Fantasía",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 8,
                    Nombre = "Musical",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 9,
                    Nombre = "Suspenso",
                });
                Entity.ToTable("Genero");
                Entity.HasData(new Generos
                {
                    GenerosId = 10,
                    Nombre = "Terror",
                });
            });

            modelBuilder.Entity<Salas>(Entity =>
            {
                Entity.ToTable("Salas");
                Entity.HasData(new Salas
                {
                    SalasId = 1,
                    Nombre = "1",
                    Capacidad = 5
                });
                Entity.HasData(new Salas
                {
                    SalasId = 2,
                    Nombre = "2",
                    Capacidad = 15
                });
                Entity.HasData(new Salas
                {
                    SalasId = 3,
                    Nombre = "3",
                    Capacidad = 35
                });
            });

            modelBuilder.Entity<Peliculas>(Entity =>
            {
                Entity.ToTable("Peliculas");
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 1,
                    Titulo = "EL REY LEÓN",
                    Poster = "https://sm.ign.com/t/ign_es/screenshot/default/d0n-cinv4aahikr_mmkj.960.jpg",
                    Sinopsis = "Tras el asesinato de su padre, Simba, un joven león es apartado su reino y tendrá que descubrir con ayuda de amigos de la sabana africana el significado de la responsabilidad y la valentía. Más tarde volverá para recuperar el control de su reino.",
                    Trailer = "https://www.youtube.com/watch?v=0U-kFH-ixV0&ab_channel=DubZone%3ALatinoam%C3%A9rica",
                    Genero = 2

                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 2,
                    Titulo = "MARIO BROS",
                    Poster = "https://i.blogs.es/617177/super-mario-bros-pelicula-mario/450_1000.webpupdate-database",
                    Sinopsis = "Dos hermanos plomeros, Mario y Luigi, caen por las alcantarillas y llegan a un mundo subterráneo mágico en el que deben enfrentarse al malvado Bowser para rescatar a la princesa Peach, quien ha sido forzada a aceptar casarse con él.",
                    Trailer = "https://www.youtube.com/watch?v=SvJwEiy2Wok&ab_channel=SensaCineTRAILERS",
                    Genero = 7
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 3,
                    Titulo = "EL GATO CON BOTAS: EL ÚLTIMO DESEO",
                    Poster = "https://es.web.img3.acsta.net/pictures/22/06/16/12/54/0504030.jpg",
                    Sinopsis = "Gato ha usado ocho de sus nueve vidas y ha perdido la cuenta. Para recuperarlas, se embarca en una gran aventura en la Selva Negra en busca de la mítica Estrella del Deseo, su última esperanza de recuperar sus vidas perdidas.",
                    Trailer = "https://www.youtube.com/watch?v=O_pRSxYuSU8&ab_channel=SensaCineTRAILERS",
                    Genero = 7
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 4,
                    Titulo = "AVENGERS ENDGAME",
                    Poster = "https://hips.hearstapps.com/hmg-prod/images/poster-vengadores-endgame-1552567490.jpg",
                    Sinopsis = "Después de los devastadores eventos ocurridos en Vengadores: Infinity War, el universo está en ruinas debido a las acciones de Thanos, el Titán Loco. Tras la derrota, las cosas no pintan bien para los Vengadores.",
                    Trailer = "https://www.youtube.com/watch?v=Oy_SER6dfK4&ab_channel=Bel%C3%A9nOrtizM.",
                    Genero = 3
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 5,
                    Titulo = "RAPIDOS Y FURIOSOS 10",
                    Poster = "https://cdn.diariojornada.com.ar/imagenes/2023/5/22/347430_1_125855_raw.jpg",
                    Sinopsis = "Dom Toretto y sus familias se enfrentan al peor enemigo imaginable, uno llegado desde el pasado con sed de venganza, dispuesto a cualquier cosa con tal de destruir todo aquello que Dom ama.",
                    Trailer = "https://www.youtube.com/watch?v=O5BOxn8Go8U&ab_channel=UniversalPicturesM%C3%A9xico",
                    Genero = 1
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 6,
                    Titulo = "SPIDERMAN: A TRAVÉS DEL SPIDER-VERSO",
                    Poster = "https://image.tmdb.org/t/p/original/pgDWrhaz0rSsD43ocNDX3PRIKJ3.jpg",
                    Sinopsis = "Después de reunirse con Gwen Stacy, el amigable vecino de tiempo completo de Brooklyn Spiderman, es lanzado a través del multiverso, donde se encuentra a un equipo de gente araña encomendada con proteger su mera existencia.",
                    Trailer = "https://www.youtube.com/watch?v=rVLFOx7AQp0&ab_channel=SensaCineTRAILERS",
                    Genero = 2
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 7,
                    Titulo = "BLONDI",
                    Poster = "https://fotos.perfil.com//2023/05/31/900/0/blondi-1579634.jpeg",
                    Sinopsis = "Blondi es una película que sigue la vida cotidiana de una joven madre y su hijo de veinte años, quienes viven como amigos compartiendo gustos por la música, las salidas nocturnas, la marihuana y el alcohol.",
                    Trailer = "https://www.youtube.com/watch?v=4TQcc7W45oA&ab_channel=TulipPictures",
                    Genero = 4
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 8,
                    Titulo = "LA MONJA 2",
                    Poster = "https://lavereda.com.ar/wp-content/uploads/2023/07/unnamed-1-691x1024.jpg",
                    Sinopsis = "Un mal se extiende en la Francia de 1956 cuando un sacerdote es asesinado y la hermana Irene se enfrenta de nuevo a la monja demoníaca Valak.",
                    Trailer = "https://www.youtube.com/watch?v=pdrPvHulyUY&ab_channel=SensaCineTRAILERS",
                    Genero = 10
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 9,
                    Titulo = "LOS DELINCUENTES",
                    Poster = "https://pics.filmaffinity.com/Los_delincuentes-192145404-large.jpg",
                    Sinopsis = "Dos empleados de banco en un determinado momento de sus vidas se cuestionan la existencia rutinaria que llevan adelante. Uno de ellos encuentra una solución, cometer un delito. De alguna manera lo logra y compromete su destino al de su compañero.",
                    Trailer = "https://www.youtube.com/watch?v=sxxbOFfagKI&ab_channel=WankaCine",
                    Genero = 9
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 10,
                    Titulo = "NO RESPIRES 2",
                    Poster = "https://elcritico.com.ar/wp-content/uploads/2021/09/no-respires-dos-poster.jpg",
                    Sinopsis = "Un veterano ciego debe usar su entrenamiento militar para salvar a un joven huérfano de un grupo de matones que irrumpen en su casa.",
                    Trailer = "https://www.youtube.com/watch?v=RjPblH5m_PY&ab_channel=SonyPicturesM%C3%A9xico",
                    Genero = 9
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 11,
                    Titulo = "BARBIE",
                    Poster = "https://i.ebayimg.com/images/g/~yMAAOSw9d5kwP9m/s-l1200.webp",
                    Sinopsis = "Después de ser expulsada de Barbieland por no ser una muñeca de aspecto perfecto, Barbie parte hacia el mundo humano para encontrar la verdadera felicidad.",
                    Trailer = "https://www.youtube.com/watch?v=gH2mRECr6y4&ab_channel=SensaCineTRAILERS",
                    Genero = 4
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 12,
                    Titulo = "LA SIRENITA",
                    Poster = "https://cloudfront-us-east-1.images.arcpublishing.com/abccolor/M7NQTVLP7NDAFJBVWGUG727LS4.jpg",
                    Sinopsis = "Una joven sirena que anhela conocer el mundo que se extiende donde acaba el mar emerge a la superficie y se enamora del príncipe Eric. Sin embargo, la única manera de estar con él exige hacer un pacto con Úrsula, la perversa bruja del mar.",
                    Trailer = "https://www.youtube.com/watch?v=8LECfkm4fJA&ab_channel=SensaCineTRAILERS",
                    Genero = 8
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 13,
                    Titulo = "ANABELLE 3: VUELVE A CASA",
                    Poster = "https://mx.web.img3.acsta.net/pictures/19/05/17/09/29/4340950.jpg",
                    Sinopsis = "Ed y Lorraine Warren intentan contener a la muñeca poseída, Annabelle, en una vitrina bendecida. Pero una noche, Annabelle despierta espíritus malignos que se obsesionan con la hija del matrimonio y sus amigos, desatando un evento sobrenatural aterrador.",
                    Trailer = "https://www.youtube.com/watch?v=KUnKWjeQA9A&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica",
                    Genero = 10
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 14,
                    Titulo = "SPIDERMAN: SIN CAMINO A CASA",
                    Poster = "https://areajugones.sport.es/wp-content/uploads/2021/11/spider-man.jpeg",
                    Sinopsis = "Nuestro héroe héroe es desenmascarado y enfrenta la difícil tarea de equilibrar su vida normal con los riesgos de ser un superhéroe. La ayuda del Doctor Strange aumenta los peligros, llevándolo a explorar el verdadero significado de ser Spider-Man.",
                    Trailer = "https://www.youtube.com/watch?v=r6t0czGbuGI&ab_channel=SonyPicturesArgentina",
                    Genero = 3
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 15,
                    Titulo = "CONTRARRELOJ",
                    Poster = "https://cinespainorg.files.wordpress.com/2023/06/screenshot_20230629_203725_twitter.jpg?w=1079",
                    Sinopsis = "Mientras Matt Turner (Liam Neeson) lleva a sus hijos a la escuela, una llamada anónima lo alerta sobre explosivos en su vehículo, desencadenando una frenética carrera contrarreloj con desafíos que desafían su ingenio y valentía para proteger a su familia.",
                    Trailer = "https://www.youtube.com/watch?v=VcuXy99TUUo&ab_channel=SensaCineTRAILERS",
                    Genero = 1
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 16,
                    Titulo = "OPPENHEIMER",
                    Poster = "https://assets.pikiran-rakyat.com/crop/0x0:0x0/x/photo/2022/07/22/4154057384.jpg",
                    Sinopsis = "Durante la Segunda Guerra Mundial, el teniente general Leslie Groves designa al físico J. Robert Oppenheimer para un grupo de trabajo que está desarrollando el Proyecto Manhattan, cuyo objetivo consiste en fabricar la primera bomba atómica.",
                    Trailer = "https://www.youtube.com/watch?v=MVvGSBKV504&ab_channel=UniversalPicturesM%C3%A9xico",
                    Genero = 6
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 17,
                    Titulo = "EL JUICIO",
                    Poster = "https://pics.filmaffinity.com/El_juicio-391136016-mmed.jpg",
                    Sinopsis = "Dos años después del fin de la dictadura militar en Argentina, los principales miembros de la junta son juzgados en los tribunales. Se trata de 18 capítulos sucintamente editados a partir de 530 horas de metraje, dando testimonio del terrorismo de Estado.",
                    Trailer = "https://www.youtube.com/watch?v=oLemh24NCEg&ab_channel=Funcinematr%C3%A1ilersyanticipos",
                    Genero = 5
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 18,
                    Titulo = "TORTUGAS NINJA CAOS MUTANTE",
                    Poster = "https://pics.filmaffinity.com/Ninja_Turtles_Caos_mutante-917947891-large.jpg",
                    Sinopsis = "Tras pasar años ocultándose, los hermanos tortuga quieren ganarse el corazón de los neoyorquinos con la ayuda de su nueva amiga, April, quien colabora con ellos en la lucha contra unos criminales. Sin embargo, terminan enfrentándose a mutantes.",
                    Trailer = "https://www.youtube.com/watch?v=VtkTHIXnFXY&ab_channel=SensaCineTRAILERS",
                    Genero = 7
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 19,
                    Titulo = "ALCARRAS",
                    Poster = "https://pics.filmaffinity.com/Alcarraas-918984157-large.jpg",
                    Sinopsis = "La vida de una familia de cultivadores de melocotones en un pequeño pueblo de Cataluña cambia cuando muere el dueño de su gran finca y su heredero vitalicio decide vender la tierra, amenazando repentinamente su sustento.",
                    Trailer = "https://www.youtube.com/watch?v=XacARMle0ZY&ab_channel=Avalon",
                    Genero = 6
                });
                Entity.HasData(new Peliculas
                {
                    Peliculasid = 20,
                    Titulo = "SONIDO DE LIBERTAD",
                    Poster = "https://www.ecartelera.com/carteles/17900/17983/001_m.jpg",
                    Sinopsis = "Un ex agente federal se embarca en una peligrosa misión para salvar a una niña de unos despiadados traficantes de menores. Se le acaba el tiempo y se adentra en la selva colombiana, arriesgando su vida para liberarla de un destino peor que la muerte.",
                    Trailer = "https://www.youtube.com/watch?v=H82uvLvszQ0&ab_channel=CanzionFilms",
                    Genero = 1
                });
            });
        }
    }
}
