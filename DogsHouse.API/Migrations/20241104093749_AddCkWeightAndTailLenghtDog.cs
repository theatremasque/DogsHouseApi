using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogsHouse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCkWeightAndTailLenghtDog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "\"CK_TailLength\"",
                table: "Dogs",
                sql: "\"TailLength\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "\"CK_Weight\"",
                table: "Dogs",
                sql: "\"Weight\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "\"CK_TailLength\"",
                table: "Dogs");

            migrationBuilder.DropCheckConstraint(
                name: "\"CK_Weight\"",
                table: "Dogs");
        }
    }
}
