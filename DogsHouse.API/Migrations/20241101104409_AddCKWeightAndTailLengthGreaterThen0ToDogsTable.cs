using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogsHouse.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCKWeightAndTailLengthGreaterThen0ToDogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "\"CK_TailLengthAndWeight\"",
                table: "Dogs",
                sql: "\"TailLength\" > 0 AND \"Weight\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "\"CK_TailLengthAndWeight\"",
                table: "Dogs");
        }
    }
}
