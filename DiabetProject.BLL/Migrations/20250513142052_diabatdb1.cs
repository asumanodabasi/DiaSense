using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetProject.BLL.Migrations
{
    /// <inheritdoc />
    public partial class diabatdb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Diagnoses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_ApplicationUserId",
                table: "Diagnoses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_ApplicationUser_ApplicationUserId",
                table: "Diagnoses",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_ApplicationUser_ApplicationUserId",
                table: "Diagnoses");

            migrationBuilder.DropIndex(
                name: "IX_Diagnoses_ApplicationUserId",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Diagnoses");
        }
    }
}
