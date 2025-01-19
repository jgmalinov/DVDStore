using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieStore.Migrations
{
    /// <inheritdoc />
    public partial class Databaseseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 4, 4, "Drama" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Background", "Name" },
                values: new object[,]
                {
                    { 1, " One of Britain's most recognisable and prolific actors, he is known for his performances on the screen and stage. Hopkins has received numerous accolades, including two Academy Awards, four BAFTA Awards, two Primetime Emmy Awards, and a Laurence Olivier Award.", "Anthony Hopkins" },
                    { 2, "Florian Zeller is a French novelist, playwright, theatre director, screenwriter, and film director. He has written over a dozen plays, that have been staged worldwide and have made him one of the most celebrated contemporary playwrights.", "Florian Zeller" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CategoryId", "DirectorId", "Price", "Price10", "Price5", "ReleaseDate", "Summary", "Title" },
                values: new object[] { 1, 4, 2, 0.0, 0.0, 0.0, new DateOnly(2020, 1, 27), "The Father is a 2020 psychological drama film, directed by Florian Zeller in his directorial debut. The film stars Anthony Hopkins as an octogenarian Welsh man living with dementia. At the 93rd Academy Awards, The Father received six nominations, including Best Picture; Hopkins won Best Actor and Zeller and Hampton won Best Adapted Screenplay. Since then, it has been cited as one of the best films of the 2020s and the 21st century.", "The Father" });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MoviesWriters",
                columns: new[] { "MovieId", "WriterId" },
                values: new object[] { 1, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesWriters",
                keyColumns: new[] { "MovieId", "WriterId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
