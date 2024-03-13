using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turtlearnMVP.Persistance.Migrations
{
    public partial class addingclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimTypeId = table.Column<int>(type: "int", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateUserId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4e214380-4f86-4422-ad7b-c0deb308e3ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5da48ab1-ece1-425c-a7e1-0c00e31b7334");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62ac2b4e-70b0-4c32-8a29-eb2590da6e8c", "AQAAAAEAACcQAAAAEGjFNqkjTm2PGM2PjU6VD6jzZxA2nZPU9eZgwGoOk38kSpRh3O0gx75PItfcueTlrQ==", "fd1294c9-8eb9-4cbd-bfda-9a20d5881e2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dee34e2f-b3d1-4aa5-bcb2-1e8fc63700e4", "AQAAAAEAACcQAAAAEGyfLlCClwyrLZfu9f08+tDez0FUKDkLymMFuDghMpdVpJhcnZYRpu0iBgWTJSVbIQ==", "cd6708e1-8d50-42a5-a0d5-81c4dc831c08" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "beb6a2ea-d512-4159-9ad9-24099e3ffd79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "08a31f78-fff5-46a4-baa6-d92bee030ad1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dae4e9fa-2b3f-4b43-94c2-e9b72767b143", "AQAAAAEAACcQAAAAEA+CFbli1MSm8pNYMB6xug7dgGbDmCCaxjTnrn9Sy/9IePPj2CimO3RxYf1wUJJ46w==", "edb20216-8ed4-4c08-bd15-59310e1b9ef3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cb31eb9-5f46-4971-b592-425505444b91", "AQAAAAEAACcQAAAAEOi0HRX+5sljr8an/7PzvXwmU6KRlPF3DYSGlNErPhfFpfdzuhY23VTbrwtR/OScow==", "8615d560-389b-4ad2-9835-f0a16a40f9e9" });
        }
    }
}
