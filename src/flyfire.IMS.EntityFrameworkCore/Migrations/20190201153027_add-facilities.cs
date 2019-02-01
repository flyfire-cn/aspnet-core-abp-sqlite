using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace flyfire.IMS.Migrations
{
    public partial class addfacilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Sqlite:InitSpatialMetaData", true);

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FacilityName = table.Column<string>(nullable: true),
                    Location = table.Column<Geometry>(nullable: true)
                        .Annotation("Sqlite:Srid", 4326),
                    LocationDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Sqlite:InitSpatialMetaData", true);
        }
    }
}
