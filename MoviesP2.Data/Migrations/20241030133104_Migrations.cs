using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesP2.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Rated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WatchedMovies",
                columns: table => new
                {
                    WatchedMovieMovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedMovies", x => x.WatchedMovieMovieId);
                    table.ForeignKey(
                        name: "FK_WatchedMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Watchlists",
                columns: table => new
                {
                    WatchlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlists", x => x.WatchlistId);
                    table.ForeignKey(
                        name: "FK_Watchlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWatchedMovie",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    WatchedMoviesWatchedMovieMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchedMovie", x => new { x.UsersUserId, x.WatchedMoviesWatchedMovieMovieId });
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchedMovie_WatchedMovies_WatchedMoviesWatchedMovieMovieId",
                        column: x => x.WatchedMoviesWatchedMovieMovieId,
                        principalTable: "WatchedMovies",
                        principalColumn: "WatchedMovieMovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieWatchlist",
                columns: table => new
                {
                    MoviesMovieId = table.Column<int>(type: "int", nullable: false),
                    WatchlistsWatchlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWatchlist", x => new { x.MoviesMovieId, x.WatchlistsWatchlistId });
                    table.ForeignKey(
                        name: "FK_MovieWatchlist_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWatchlist_Watchlists_WatchlistsWatchlistId",
                        column: x => x.WatchlistsWatchlistId,
                        principalTable: "Watchlists",
                        principalColumn: "WatchlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieWatchlist_WatchlistsWatchlistId",
                table: "MovieWatchlist",
                column: "WatchlistsWatchlistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchedMovie_WatchedMoviesWatchedMovieMovieId",
                table: "UserWatchedMovie",
                column: "WatchedMoviesWatchedMovieMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedMovies_MovieId",
                table: "WatchedMovies",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_UserId",
                table: "Watchlists",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieWatchlist");

            migrationBuilder.DropTable(
                name: "UserWatchedMovie");

            migrationBuilder.DropTable(
                name: "Watchlists");

            migrationBuilder.DropTable(
                name: "WatchedMovies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
