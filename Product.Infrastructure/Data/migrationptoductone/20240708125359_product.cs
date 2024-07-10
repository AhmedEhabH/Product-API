using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Infrastructure.Data.migrationptoductone
{
    /// <inheritdoc />
    public partial class product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductPicture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductPicture",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
