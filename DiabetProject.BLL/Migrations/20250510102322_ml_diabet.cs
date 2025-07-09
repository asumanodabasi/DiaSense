using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetProject.BLL.Migrations
{
    /// <inheritdoc />
    public partial class ml_diabet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<float>(type: "real", nullable: false),
                    Hypertension = table.Column<int>(type: "int", nullable: false),
                    HeartDisease = table.Column<int>(type: "int", nullable: false),
                    SmokingHistory = table.Column<int>(type: "int", nullable: false),
                    Bmi = table.Column<float>(type: "real", nullable: false),
                    HbA1cLevel = table.Column<float>(type: "real", nullable: false),
                    BloodGlucoseLevel = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnoses");
        }
    }
}
