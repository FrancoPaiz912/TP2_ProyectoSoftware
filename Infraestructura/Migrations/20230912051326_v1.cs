using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    GenerosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.GenerosId);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    SalasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.SalasId);
                });

            migrationBuilder.CreateTable(
                name: "Peliculas",
                columns: table => new
                {
                    Peliculasid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sinopsis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peliculas", x => x.Peliculasid);
                    table.ForeignKey(
                        name: "FK_Peliculas_Genero_Genero",
                        column: x => x.Genero,
                        principalTable: "Genero",
                        principalColumn: "GenerosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funciones",
                columns: table => new
                {
                    FuncionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tiempo = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funciones", x => x.FuncionesId);
                    table.ForeignKey(
                        name: "FK_Funciones_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Peliculasid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funciones_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "SalasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionId = table.Column<int>(type: "int", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketsId);
                    table.ForeignKey(
                        name: "FK_Tickets_Funciones_FuncionId",
                        column: x => x.FuncionId,
                        principalTable: "Funciones",
                        principalColumn: "FuncionesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "GenerosId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Acción" },
                    { 2, "Aventuras" },
                    { 3, "Ciencia Ficción" },
                    { 4, "Comedia" },
                    { 5, "Documental" },
                    { 6, "Drama" },
                    { 7, "Fantasía" },
                    { 8, "Musical" },
                    { 9, "Suspenso" },
                    { 10, "Terror" }
                });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "SalasId", "Capacidad", "Nombre" },
                values: new object[,]
                {
                    { 1, 5, "1" },
                    { 2, 15, "2" },
                    { 3, 35, "3" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Peliculasid", "Genero", "Poster", "Sinopsis", "Titulo", "Trailer" },
                values: new object[,]
                {
                    { 1, 2, "https://sm.ign.com/t/ign_es/screenshot/default/d0n-cinv4aahikr_mmkj.960.jpg", "Tras el asesinato de su padre, Simba, un joven león es apartado su reino y tendrá que descubrir con ayuda de amigos de la sabana africana el significado de la responsabilidad y la valentía. Más tarde volverá para recuperar el control de su reino.", "EL REY LEÓN", "https://www.youtube.com/watch?v=0U-kFH-ixV0&ab_channel=DubZone%3ALatinoam%C3%A9rica" },
                    { 2, 7, "https://i.blogs.es/617177/super-mario-bros-pelicula-mario/450_1000.webpupdate-database", "Dos hermanos plomeros, Mario y Luigi, caen por las alcantarillas y llegan a un mundo subterráneo mágico en el que deben enfrentarse al malvado Bowser para rescatar a la princesa Peach, quien ha sido forzada a aceptar casarse con él.", "MARIO BROS", "https://www.youtube.com/watch?v=SvJwEiy2Wok&ab_channel=SensaCineTRAILERS" },
                    { 3, 7, "https://es.web.img3.acsta.net/pictures/22/06/16/12/54/0504030.jpg", "Gato ha usado ocho de sus nueve vidas y ha perdido la cuenta. Para recuperarlas, se embarca en una gran aventura en la Selva Negra en busca de la mítica Estrella del Deseo, su última esperanza de recuperar sus vidas perdidas.", "EL GATO CON BOTAS: EL ÚLTIMO DESEO", "https://www.youtube.com/watch?v=O_pRSxYuSU8&ab_channel=SensaCineTRAILERS" },
                    { 4, 3, "https://hips.hearstapps.com/hmg-prod/images/poster-vengadores-endgame-1552567490.jpg", "Después de los devastadores eventos ocurridos en Vengadores: Infinity War, el universo está en ruinas debido a las acciones de Thanos, el Titán Loco. Tras la derrota, las cosas no pintan bien para los Vengadores.", "AVENGERS ENDGAME", "https://www.youtube.com/watch?v=Oy_SER6dfK4&ab_channel=Bel%C3%A9nOrtizM." },
                    { 5, 1, "https://cdn.diariojornada.com.ar/imagenes/2023/5/22/347430_1_125855_raw.jpg", "Dom Toretto y sus familias se enfrentan al peor enemigo imaginable, uno llegado desde el pasado con sed de venganza, dispuesto a cualquier cosa con tal de destruir todo aquello que Dom ama.", "RAPIDOS Y FURIOSOS 10", "https://www.youtube.com/watch?v=O5BOxn8Go8U&ab_channel=UniversalPicturesM%C3%A9xico" },
                    { 6, 2, "https://image.tmdb.org/t/p/original/pgDWrhaz0rSsD43ocNDX3PRIKJ3.jpg", "Después de reunirse con Gwen Stacy, el amigable vecino de tiempo completo de Brooklyn Spiderman, es lanzado a través del multiverso, donde se encuentra a un equipo de gente araña encomendada con proteger su mera existencia.", "SPIDERMAN: A TRAVÉS DEL SPIDER-VERSO", "https://www.youtube.com/watch?v=rVLFOx7AQp0&ab_channel=SensaCineTRAILERS" },
                    { 7, 4, "https://fotos.perfil.com//2023/05/31/900/0/blondi-1579634.jpeg", "Blondi es una película que sigue la vida cotidiana de una joven madre y su hijo de veinte años, quienes viven como amigos compartiendo gustos por la música, las salidas nocturnas, la marihuana y el alcohol.", "BLONDI", "https://www.youtube.com/watch?v=4TQcc7W45oA&ab_channel=TulipPictures" },
                    { 8, 10, "https://lavereda.com.ar/wp-content/uploads/2023/07/unnamed-1-691x1024.jpg", "Un mal se extiende en la Francia de 1956 cuando un sacerdote es asesinado y la hermana Irene se enfrenta de nuevo a la monja demoníaca Valak.", "LA MONJA 2", "https://www.youtube.com/watch?v=pdrPvHulyUY&ab_channel=SensaCineTRAILERS" },
                    { 9, 9, "https://pics.filmaffinity.com/Los_delincuentes-192145404-large.jpg", "Dos empleados de banco en un determinado momento de sus vidas se cuestionan la existencia rutinaria que llevan adelante. Uno de ellos encuentra una solución, cometer un delito. De alguna manera lo logra y compromete su destino al de su compañero.", "LOS DELINCUENTES", "https://www.youtube.com/watch?v=sxxbOFfagKI&ab_channel=WankaCine" },
                    { 10, 9, "https://elcritico.com.ar/wp-content/uploads/2021/09/no-respires-dos-poster.jpg", "Un veterano ciego debe usar su entrenamiento militar para salvar a un joven huérfano de un grupo de matones que irrumpen en su casa.", "NO RESPIRES 2", "https://www.youtube.com/watch?v=RjPblH5m_PY&ab_channel=SonyPicturesM%C3%A9xico" },
                    { 11, 4, "https://i.ebayimg.com/images/g/~yMAAOSw9d5kwP9m/s-l1200.webp", "Después de ser expulsada de Barbieland por no ser una muñeca de aspecto perfecto, Barbie parte hacia el mundo humano para encontrar la verdadera felicidad.", "BARBIE", "https://www.youtube.com/watch?v=gH2mRECr6y4&ab_channel=SensaCineTRAILERS" },
                    { 12, 8, "https://cloudfront-us-east-1.images.arcpublishing.com/abccolor/M7NQTVLP7NDAFJBVWGUG727LS4.jpg", "Una joven sirena que anhela conocer el mundo que se extiende donde acaba el mar emerge a la superficie y se enamora del príncipe Eric. Sin embargo, la única manera de estar con él exige hacer un pacto con Úrsula, la perversa bruja del mar.", "LA SIRENITA", "https://www.youtube.com/watch?v=8LECfkm4fJA&ab_channel=SensaCineTRAILERS" },
                    { 13, 10, "https://mx.web.img3.acsta.net/pictures/19/05/17/09/29/4340950.jpg", "Ed y Lorraine Warren intentan contener a la muñeca poseída, Annabelle, en una vitrina bendecida. Pero una noche, Annabelle despierta espíritus malignos que se obsesionan con la hija del matrimonio y sus amigos, desatando un evento sobrenatural aterrador.", "ANABELLE 3: VUELVE A CASA", "https://www.youtube.com/watch?v=KUnKWjeQA9A&ab_channel=WarnerBros.PicturesLatinoam%C3%A9rica" },
                    { 14, 3, "https://areajugones.sport.es/wp-content/uploads/2021/11/spider-man.jpeg", "Nuestro héroe héroe es desenmascarado y enfrenta la difícil tarea de equilibrar su vida normal con los riesgos de ser un superhéroe. La ayuda del Doctor Strange aumenta los peligros, llevándolo a explorar el verdadero significado de ser Spider-Man.", "SPIDERMAN: SIN CAMINO A CASA", "https://www.youtube.com/watch?v=r6t0czGbuGI&ab_channel=SonyPicturesArgentina" },
                    { 15, 1, "https://cinespainorg.files.wordpress.com/2023/06/screenshot_20230629_203725_twitter.jpg?w=1079", "Mientras Matt Turner (Liam Neeson) lleva a sus hijos a la escuela, una llamada anónima lo alerta sobre explosivos en su vehículo, desencadenando una frenética carrera contrarreloj con desafíos que desafían su ingenio y valentía para proteger a su familia.", "CONTRARRELOJ", "https://www.youtube.com/watch?v=VcuXy99TUUo&ab_channel=SensaCineTRAILERS" },
                    { 16, 6, "https://assets.pikiran-rakyat.com/crop/0x0:0x0/x/photo/2022/07/22/4154057384.jpg", "Durante la Segunda Guerra Mundial, el teniente general Leslie Groves designa al físico J. Robert Oppenheimer para un grupo de trabajo que está desarrollando el Proyecto Manhattan, cuyo objetivo consiste en fabricar la primera bomba atómica.", "OPPENHEIMER", "https://www.youtube.com/watch?v=MVvGSBKV504&ab_channel=UniversalPicturesM%C3%A9xico" },
                    { 17, 5, "https://pics.filmaffinity.com/El_juicio-391136016-mmed.jpg", "Dos años después del fin de la dictadura militar en Argentina, los principales miembros de la junta son juzgados en los tribunales. Se trata de 18 capítulos sucintamente editados a partir de 530 horas de metraje, dando testimonio del terrorismo de Estado.", "EL JUICIO", "https://www.youtube.com/watch?v=oLemh24NCEg&ab_channel=Funcinematr%C3%A1ilersyanticipos" },
                    { 18, 7, "https://pics.filmaffinity.com/Ninja_Turtles_Caos_mutante-917947891-large.jpg", "Tras pasar años ocultándose, los hermanos tortuga quieren ganarse el corazón de los neoyorquinos con la ayuda de su nueva amiga, April, quien colabora con ellos en la lucha contra unos criminales. Sin embargo, terminan enfrentándose a mutantes.", "TORTUGAS NINJA CAOS MUTANTE", "https://www.youtube.com/watch?v=VtkTHIXnFXY&ab_channel=SensaCineTRAILERS" },
                    { 19, 6, "https://pics.filmaffinity.com/Alcarraas-918984157-large.jpg", "La vida de una familia de cultivadores de melocotones en un pequeño pueblo de Cataluña cambia cuando muere el dueño de su gran finca y su heredero vitalicio decide vender la tierra, amenazando repentinamente su sustento.", "ALCARRAS", "https://www.youtube.com/watch?v=XacARMle0ZY&ab_channel=Avalon" },
                    { 20, 1, "https://www.ecartelera.com/carteles/17900/17983/001_m.jpg", "Un ex agente federal se embarca en una peligrosa misión para salvar a una niña de unos despiadados traficantes de menores. Se le acaba el tiempo y se adentra en la selva colombiana, arriesgando su vida para liberarla de un destino peor que la muerte.", "SONIDO DE LIBERTAD", "https://www.youtube.com/watch?v=H82uvLvszQ0&ab_channel=CanzionFilms" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_PeliculaId",
                table: "Funciones",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funciones_SalaId",
                table: "Funciones",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_Genero",
                table: "Peliculas",
                column: "Genero");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FuncionId",
                table: "Tickets",
                column: "FuncionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketsId",
                table: "Tickets",
                column: "TicketsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Funciones");

            migrationBuilder.DropTable(
                name: "Peliculas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Genero");
        }
    }
}
