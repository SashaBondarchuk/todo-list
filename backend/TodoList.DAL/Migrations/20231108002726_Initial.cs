using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoList.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "Noe60@hotmail.com", "pl2r67AEAFyf93hI7xpQUfbQiWZT4+SqkB8kpb3ZQ60acBca", "Vanessa_VonRueden" },
                    { 2, "Tiffany38@gmail.com", "f/2ghg0oJMcWEBu6kcc6tQXkUjbQeiGgQ54fZteA1x0jpO+i", "Dannie_Zieme" },
                    { 3, "Amiya62@yahoo.com", "jczOv5adAUQ9aOP5DSgyK7DDvYoZ8aq0PpkWpEGpaTQoMv2n", "Lisandro12" },
                    { 4, "Hailee96@hotmail.com", "yjbIuAEyApdjspeMUj+d7ysnTJj+QNgHwQKORCjSaux+G8bs", "Gussie54" },
                    { 5, "Gregoria.Kunze89@hotmail.com", "SlnevLDroH7d5ehue2fCJhPP484Jdrp3eTxWeox2yeMgJ21K", "Guido.Krajcik" },
                    { 6, "Aliyah14@gmail.com", "dAK+hXRdevAeNafm9NFs3uWKU6X7WPN3/Qmc0rR2vy+qNXGv", "Ephraim_Emmerich" },
                    { 7, "Zita_Spinka@gmail.com", "gmM2F1OHpMZFWg0CvFkrTU79rYZD+tsqeifIIx4s4ed8xKWq", "Deonte.Buckridge86" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "Description", "IsCompleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Unde voluptatem repellendus quasi voluptatem atque eos vel dignissimos eveniet.", false, "Fugiat delectus fugit.", 4 },
                    { 2, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et asperiores odio doloribus est sit numquam minima delectus et.", false, "Unde aliquid ex sit iusto dignissimos aut qui quod.", 3 },
                    { 3, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iure saepe omnis corporis ex explicabo vero qui occaecati aut.", false, "Repellat nesciunt voluptatem sunt labore odio et.", 4 },
                    { 4, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Incidunt et accusamus tempora fugiat asperiores omnis ea.", true, "Ipsum beatae quo quos numquam.", 6 },
                    { 5, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ex aut dignissimos.", true, "Et quis labore earum incidunt aspernatur soluta nobis nostrum.", 3 },
                    { 6, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sint nam molestiae enim deserunt.", true, "Ut dolorem vitae odio vitae voluptatem consequatur ut.", 6 },
                    { 7, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Velit earum reiciendis doloremque aut et ut.", false, "Et ratione rerum.", 6 },
                    { 8, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus voluptas voluptatum aut officia quo iusto facere autem.", false, "Ipsum ipsa magnam non provident earum.", 6 },
                    { 9, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Voluptate ad aut.", false, "Est autem voluptates et quia qui rerum.", 3 },
                    { 10, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eos nemo voluptate mollitia dolorem.", true, "Sunt dolores aut inventore et quia necessitatibus.", 3 },
                    { 11, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Voluptatem dolores architecto nihil facilis quo dignissimos excepturi.", true, "Molestiae tempora consectetur ex a.", 4 },
                    { 12, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sint non animi et culpa repellat distinctio error.", false, "Delectus fuga itaque accusamus voluptatem harum qui.", 2 },
                    { 13, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aliquid similique velit perferendis vero id id voluptate aut.", true, "Beatae eaque nam blanditiis incidunt placeat voluptatibus commodi error illo.", 6 },
                    { 14, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nam eos consequuntur perspiciatis corporis.", false, "Dignissimos cumque distinctio quidem dolores nihil maiores enim.", 1 },
                    { 15, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cumque culpa doloremque ullam similique qui inventore.", true, "Suscipit ut culpa.", 5 },
                    { 16, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Culpa repellendus quas nobis nihil architecto totam omnis.", false, "Veniam doloremque ipsam eius possimus similique fugit corrupti.", 6 },
                    { 17, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harum suscipit non.", true, "Est voluptas totam ratione maxime.", 7 },
                    { 18, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Velit eos aut.", false, "Officia non culpa explicabo incidunt.", 7 },
                    { 19, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tempora aspernatur officia dolorum et.", false, "Autem velit eveniet.", 5 },
                    { 20, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Facere consequuntur reiciendis ut totam accusantium possimus enim molestiae porro.", true, "Natus ipsum voluptatum quidem itaque omnis iste explicabo est rem.", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
