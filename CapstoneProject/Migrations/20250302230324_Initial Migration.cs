using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CapstoneProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Truckers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TruckerId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalSpent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truckers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Truckers_TruckerId",
                        column: x => x.TruckerId,
                        principalTable: "Truckers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Truckers_TruckerId",
                        column: x => x.TruckerId,
                        principalTable: "Truckers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TruckerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Truckers_TruckerId",
                        column: x => x.TruckerId,
                        principalTable: "Truckers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "item1.jpg", "Tire", 120.00m },
                    { 2, "item2.jpeg", "Oil", 40.00m },
                    { 3, "item3.jpg", "Brake Pads", 70.00m },
                    { 4, "item4.jpg", "Wiper Blades", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Truckers",
                columns: new[] { "Id", "FirstName", "LastName", "TotalSpent", "TruckerId" },
                values: new object[,]
                {
                    { 1, "Kuljeet", "Singh Sidhu", 0m, "9362" },
                    { 2, "Gurpreet", "Singh Kooner", 0m, "9386" },
                    { 3, "Luis", "S Azevedo", 0m, "9394" },
                    { 4, "Luis", "S Azevedo", 0m, "9406" },
                    { 5, "Ravinder", "Jaswal", 0m, "9412" },
                    { 6, "Yadwinder", "Singh", 0m, "9414" },
                    { 7, "Manjinder", "Singh", 0m, "9416" },
                    { 8, "Yadwinder", "Singh", 0m, "9420" },
                    { 9, "Yadwinder", "Singh", 0m, "9422" },
                    { 10, "Jatinder", "Singh", 0m, "9424" },
                    { 11, "Ravinder", "Jaswal", 0m, "9430" },
                    { 12, "Ravinder", "Jaswal", 0m, "9432" },
                    { 13, "Preetinder", "Singh", 0m, "9436" },
                    { 14, "Tarinder", "Singh Dosanjh", 0m, "9438" },
                    { 15, "Balwinder", "Singh Sandhu", 0m, "9440" },
                    { 16, "Tarinder", "Singh Dosanjh", 0m, "9442" },
                    { 17, "Jasmeet", "Singh Grewal", 0m, "9446" },
                    { 18, "Simratpal", "Singh", 0m, "9452" },
                    { 19, "Harjinder", "Singh", 0m, "9460" },
                    { 20, "Sidhant", "Sharda", 0m, "9462" },
                    { 21, "Chetanbir", "Sohal", 0m, "9464" },
                    { 22, "Ravinder", "Jaswal", 0m, "9466" },
                    { 23, "Ravinder", "Jaswal", 0m, "9468" },
                    { 24, "Ravinder", "Jaswal", 0m, "9470" },
                    { 25, "Ravinder", "Jaswal", 0m, "9472" },
                    { 26, "Hardeep", "Singh", 0m, "9474" },
                    { 27, "Hardeep", "Singh", 0m, "9476" },
                    { 28, "Lawan", "Kapoor", 0m, "9478" },
                    { 29, "Tarinder", "Singh Dosanjh", 0m, "9480" },
                    { 30, "Balkaran", "Dhillon", 0m, "9484" },
                    { 31, "Balkaran", "Dhillon", 0m, "9486" },
                    { 32, "Yadwinder", "Singh", 0m, "9488" },
                    { 33, "Jasmeet", "Singh Grewal", 0m, "9490" },
                    { 34, "Gurpreet", "Singh Kooner", 0m, "9492" },
                    { 35, "Balkaran", "Dhillon", 0m, "9494" },
                    { 36, "Amanjot", "Singh", 0m, "9498" },
                    { 37, "Balkaran", "Dhillon", 0m, "9500" },
                    { 38, "Kuljit", "Singh .", 0m, "9502" },
                    { 39, "Gurpreet", "Singh Dhillon", 0m, "9504" },
                    { 40, "Ravinder", "Jaswal", 0m, "9506" },
                    { 41, "Ravinder", "Jaswal", 0m, "9508" },
                    { 42, "Ravinder", "Jaswal", 0m, "9510" },
                    { 43, "Ravinder", "Jaswal", 0m, "9512" },
                    { 44, "Gurpreet", "Singh Kooner", 0m, "9514" },
                    { 45, "Balkaran", "Dhillon", 0m, "9516" },
                    { 46, "Gurpreet", "Singh Dhillon", 0m, "9518" },
                    { 47, "Akashdeep", "Singh", 0m, "9522" },
                    { 48, "Roop Kamal", "Singh Mand", 0m, "9526" },
                    { 49, "Harpreet", "Singh", 0m, "9534" },
                    { 50, "Gurpreet", "Singh Kooner", 0m, "9538" },
                    { 51, "Manjinder", "Singh", 0m, "9544" },
                    { 52, "Balkaran", "Dhillon", 0m, "9546" },
                    { 53, "Vikramjit", "Singh Sandhu", 0m, "9548" },
                    { 54, "Jobanpreet", "Singh", 0m, "9552" },
                    { 55, "Navpreet", "Singh Randhay", 0m, "9554" },
                    { 56, "Akashdeep", "Singh", 0m, "9556" },
                    { 57, "Yadwinder", "Singh", 0m, "156" },
                    { 58, "Jatinder", "Singh Bains", 0m, "158" },
                    { 59, "Yadwinder", "Singh", 0m, "162" },
                    { 60, "Yadwinder", "Singh", 0m, "164" },
                    { 61, "Paramjit", "Singh Ghuman", 0m, "170" },
                    { 62, "Luis", "S Azevedo", 0m, "174" },
                    { 63, "Luis", "S Azevedo", 0m, "176" },
                    { 64, "Luis", "S Azevedo", 0m, "178" },
                    { 65, "Luis", "S Azevedo", 0m, "186" },
                    { 66, "Luis", "S Azevedo", 0m, "188" },
                    { 67, "Chamkaur", "Singh Kaile", 0m, "189" },
                    { 68, "Luis", "S Azevedo", 0m, "190" },
                    { 69, "Sukhpreet", "Singh", 0m, "194" },
                    { 70, "Harcharan", "Singh", 0m, "198" },
                    { 71, "Manjinder", "Singh", 0m, "200" },
                    { 72, "Kelly", "Fagundes Giampapa", 0m, "202" },
                    { 73, "Harmanpreet", "Bajwa", 0m, "204" },
                    { 74, "Jaspal", "Gill", 0m, "206" },
                    { 75, "Jaskarn", "Singh", 0m, "208" },
                    { 76, "Luis", "S Azevedo", 0m, "212" },
                    { 77, "Rajender", "Singh Dhesi", 0m, "214" },
                    { 78, "Sami", "Mohamed Gharbiya", 0m, "220" },
                    { 79, "Luis", "S Azevedo", 0m, "222" },
                    { 80, "Luis", "S Azevedo", 0m, "224" },
                    { 81, "Kelly", "Fagundes Giampapa", 0m, "226" },
                    { 82, "Pardeep", "Singh Grewal", 0m, "228" },
                    { 83, "Kelly", "Fagundes Giampapa", 0m, "230" },
                    { 84, "Pardeep", "Singh Grewal", 0m, "234" },
                    { 85, "Pardeep", "Singh Grewal", 0m, "236" },
                    { 86, "Lovepreet", "Singh", 0m, "238" },
                    { 87, "Yadwinder", "Singh", 0m, "240" },
                    { 88, "Chetanbir", "Sohal", 0m, "244" },
                    { 89, "Jaskarn", "Singh", 0m, "246" },
                    { 90, "Ravinder", "Jaswal", 0m, "250" },
                    { 91, "Ravinder", "Jaswal", 0m, "252" },
                    { 92, "Manveer", "Singh", 0m, "258" },
                    { 93, "Jagmeet", "Singh", 0m, "260" },
                    { 94, "Gurjit", "Singh Chahal", 0m, "262" },
                    { 95, "Jaskarn", "Singh", 0m, "264" },
                    { 96, "Chetanbir", "Sohal", 0m, "266" },
                    { 97, "Chetanbir", "Sohal", 0m, "270" },
                    { 98, "Chetanbir", "Sohal", 0m, "272" },
                    { 99, "Chetanbir", "Sohal", 0m, "274" },
                    { 100, "Kelly", "Fagundes Giampapa", 0m, "276" },
                    { 101, "Kelly", "Fagundes Giampapa", 0m, "278" },
                    { 102, "Kelly", "Fagundes Giampapa", 0m, "280" },
                    { 103, "Luis", "S Azevedo", 0m, "282" },
                    { 104, "Gurpreet", "Singh Dhillon", 0m, "284" },
                    { 105, "Pawanpreet", "Singh", 0m, "286" },
                    { 106, "Ravinder", "Jaswal", 0m, "288" },
                    { 107, "Ravinder", "Jaswal", 0m, "290" },
                    { 108, "Ravinder", "Jaswal", 0m, "292" },
                    { 109, "Gurvinder", "Pannu", 0m, "294" },
                    { 110, "Hardeep", "Singh", 0m, "300" },
                    { 111, "Hardeep", "Singh", 0m, "302" },
                    { 112, "Chetanbir", "Sohal", 0m, "304" },
                    { 113, "Satnam", "Singh Saini", 0m, "312" },
                    { 114, "Hardeep", "Singh Dhaliwal", 0m, "314" },
                    { 115, "Kulwinder", "Singh Bagga", 0m, "316" },
                    { 116, "Amrik", "Singh", 0m, "318" },
                    { 117, "Karminder", "Singh Cheema", 0m, "324" },
                    { 118, "Pardeep", "Singh Grewal", 0m, "326" },
                    { 119, "Hardeep", "Singh", 0m, "328" },
                    { 120, "Hardeep", "Singh", 0m, "330" },
                    { 121, "Hardeep", "Singh", 0m, "332" },
                    { 122, "Himmat", "Singh", 0m, "334" },
                    { 123, "Hardeep", "Singh", 0m, "336" },
                    { 124, "Pardeep", "Singh Grewal", 0m, "338" },
                    { 125, "Balraj", "Singh Gill", 0m, "340" },
                    { 126, "Manveer", "Singh", 0m, "342" },
                    { 127, "Pardeep", "Singh Grewal", 0m, "344" },
                    { 128, "Bahadur", "Singh Thiara", 0m, "1320" },
                    { 129, "Rajvir", "Singh Sagi", 0m, "1339" },
                    { 130, "Hardeep 1416", "Singh", 0m, "1416" },
                    { 131, "Jaskirandeep", "Singh Gill", 0m, "1524" },
                    { 132, "Chaudhery Amir", "Amin Bajwa", 0m, "1550" },
                    { 133, "Gurvinder", "Singh Kahlon", 0m, "1620" },
                    { 134, "Kulwant", "Sandhu", 0m, "1628" },
                    { 135, "Gurbhej", "Singh Kahlon", 0m, "1642" },
                    { 136, "Gurpreet", "Singh Shergill", 0m, "1646" },
                    { 137, "Jan", "Wyzga", 0m, "1658" },
                    { 138, "Mervin Walton", "Robinson", 0m, "1660" },
                    { 139, "Jun", "Zhu", 0m, "1662" },
                    { 140, "Kulwant Singh", "Sandhu", 0m, "1664" },
                    { 141, "Kulwant Singh", "Sandhu", 0m, "1666" },
                    { 142, "Radhe Sham", "Thind", 0m, "904150" },
                    { 143, "Shahid", "Rashid", 0m, "907120" },
                    { 144, "Kanwaljot Singh", "Sandhu", 0m, "916136" },
                    { 145, "Jaspal", "Brar", 0m, "919128" },
                    { 146, "Ishneet Paul", "Singh Sandhu", 0m, "919134" },
                    { 147, "Jaspal", "Brar", 0m, "919135" },
                    { 148, "Jaspal", "Brar", 0m, "919137" },
                    { 149, "Jaspal", "Brar", 0m, "919138" },
                    { 150, "Zauhar Alankar", "Gill", 0m, "920127" },
                    { 151, "Kapil Kumar", "Muttan", 0m, "920142" },
                    { 152, "Sumanjot", "Singh", 0m, "920145" },
                    { 153, "Simranjeet", "Singh", 0m, "920155" },
                    { 154, "Kanwarbir", "Singh", 0m, "920157" },
                    { 155, "Akshay", "Kumar", 0m, "921154" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TruckerId",
                table: "Invoices",
                column: "TruckerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TruckerId",
                table: "Orders",
                column: "TruckerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ItemId",
                table: "Transactions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TruckerId",
                table: "Transactions",
                column: "TruckerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Truckers");
        }
    }
}
