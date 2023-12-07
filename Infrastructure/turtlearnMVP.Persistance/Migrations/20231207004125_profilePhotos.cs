using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turtlearnMVP.Persistance.Migrations
{
    public partial class profilePhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "03b8e4cb-dc86-4452-977e-d44506eb4cc8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "37f471fc-7e54-4c34-b440-21a0200a8bc6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Photo", "SecurityStamp" },
                values: new object[] { "3328d126-99e2-4f1e-ba70-c0254296ea3d", "AQAAAAEAACcQAAAAEAEbqzdCBLgeTNNAORsoNpDL9t5hamLOQl4+LbIp1IL0PYHVvbdAZCzGuRoAlMsc5g==", "https://pbs.twimg.com/media/C8QqGm4UQAAUiET.jpg", "4cb7a871-604d-453a-8e85-74676ebeef05" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "Photo", "SecurityStamp" },
                values: new object[] { "1f65dd60-6f8f-419c-bcc1-a08bd3bb78c6", "editor ", "AQAAAAEAACcQAAAAEAQM6fU7hJPl1C18tLa6ass9xXsD19/qz7/qJ6b8QMT//0unEQsXlnJ9n1mvPwCRAA==", "https://pbs.twimg.com/media/C8QqGm4UQAAUiET.jpg", "01cd78ec-c099-4259-8d68-7cd3e538dc61" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0cb356c5-096a-483e-8cdb-5799ec195da0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0dfc3ec5-0809-4d05-bff8-b49ef6ada386");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Photo", "SecurityStamp" },
                values: new object[] { "f00393e1-f00c-4c89-87b8-ed96141fc28f", "AQAAAAEAACcQAAAAEF11B8UeMb+/apwEwmGlfuLsNziRidwegSf5v2NNYYtYb6/sPINV79edMJ+cLi4LMw==", null, "0601f581-51fb-44c6-b083-931a80c9e737" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "LastName", "PasswordHash", "Photo", "SecurityStamp" },
                values: new object[] { "1efeb737-f24a-49b3-b77b-a6ab9ba6a884", "editor", "AQAAAAEAACcQAAAAEHK0SIOUUNJHstORocvgHU83JxS/CWhh1MW9d4xBeYsUPDR2MyqPXALtmK/f2CPa/g==", null, "c5913485-ae98-4488-ac74-92f205188c2c" });
        }
    }
}
