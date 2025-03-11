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
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTopAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TruckerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Truckers_TruckerId",
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
                table: "AdminUsers",
                columns: new[] { "Id", "IsTopAdmin", "Password", "Username" },
                values: new object[] { 1, true, "Password123", "Admin" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "item1.jpg", "Tire", 120.00m, 0 },
                    { 2, "item2.jpeg", "Oil", 40.00m, 0 },
                    { 3, "item3.jpg", "Brake Pads", 70.00m, 0 },
                    { 4, "item4.jpg", "Wiper Blades", 25.00m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Truckers",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Kuljeet", "Singh Sidhu" },
                    { 2, "Gurpreet", "Singh Kooner" },
                    { 3, "Luis", "S Azevedo" },
                    { 4, "Ravinder", "Jaswal" },
                    { 5, "Yadwinder", "Singh" },
                    { 6, "Manjinder", "Singh" },
                    { 7, "Jatinder", "Singh" },
                    { 8, "Preetinder", "Singh" },
                    { 9, "Tarinder", "Singh Dosanjh" },
                    { 10, "Balwinder", "Singh Sandhu" },
                    { 11, "Jasmeet", "Singh Grewal" },
                    { 12, "Simratpal", "Singh" },
                    { 13, "Harjinder", "Singh" },
                    { 14, "Sidhant", "Sharda" },
                    { 15, "Chetanbir", "Sohal" },
                    { 16, "Hardeep", "Singh" },
                    { 17, "Lawan", "Kapoor" },
                    { 18, "Balkaran", "Dhillon" },
                    { 19, "Amanjot", "Singh" },
                    { 20, "Kuljit", "Singh ." },
                    { 21, "Gurpreet", "Singh Dhillon" },
                    { 22, "Akashdeep", "Singh" },
                    { 23, "Roop Kamal", "Singh Mand" },
                    { 24, "Harpreet", "Singh" },
                    { 25, "Vikramjit", "Singh Sandhu" },
                    { 26, "Jobanpreet", "Singh" },
                    { 27, "Navpreet", "Singh Randhay" },
                    { 28, "Jatinder", "Singh Bains" },
                    { 29, "Paramjit", "Singh Ghuman" },
                    { 30, "Chamkaur", "Singh Kaile" },
                    { 31, "Sukhpreet", "Singh" },
                    { 32, "Harcharan", "Singh" },
                    { 33, "Kelly", "Fagundes Giampapa" },
                    { 34, "Harmanpreet", "Bajwa" },
                    { 35, "Jaspal", "Gill" },
                    { 36, "Jaskarn", "Singh" },
                    { 37, "Rajender", "Singh Dhesi" },
                    { 38, "Sami", "Mohamed Gharbiya" },
                    { 39, "Pardeep", "Singh Grewal" },
                    { 40, "Lovepreet", "Singh" },
                    { 41, "Manveer", "Singh" },
                    { 42, "Jagmeet", "Singh" },
                    { 43, "Gurjit", "Singh Chahal" },
                    { 44, "Pawanpreet", "Singh" },
                    { 45, "Gurvinder", "Pannu" },
                    { 46, "Satnam", "Singh Saini" },
                    { 47, "Hardeep", "Singh Dhaliwal" },
                    { 48, "Kulwinder", "Singh Bagga" },
                    { 49, "Amrik", "Singh" },
                    { 50, "Karminder", "Singh Cheema" },
                    { 51, "Himmat", "Singh" },
                    { 52, "Balraj", "Singh Gill" },
                    { 53, "Bahadur", "Singh Thiara" },
                    { 54, "Rajvir", "Singh Sagi" },
                    { 55, "Hardeep 1416", "Singh" },
                    { 56, "Jaskirandeep", "Singh Gill" },
                    { 57, "Chaudhery Amir", "Amin Bajwa" },
                    { 58, "Gurvinder", "Singh Kahlon" },
                    { 59, "Kulwant", "Sandhu" },
                    { 60, "Gurbhej", "Singh Kahlon" },
                    { 61, "Gurpreet", "Singh Shergill" },
                    { 62, "Jan", "Wyzga" },
                    { 63, "Mervin Walton", "Robinson" },
                    { 64, "Jun", "Zhu" },
                    { 65, "Kulwant Singh", "Sandhu" },
                    { 66, "Radhe Sham", "Thind" },
                    { 67, "Shahid", "Rashid" },
                    { 68, "Kanwaljot Singh", "Sandhu" },
                    { 69, "Jaspal", "Brar" },
                    { 70, "Ishneet Paul", "Singh Sandhu" },
                    { 71, "Zauhar Alankar", "Gill" },
                    { 72, "Kapil Kumar", "Muttan" },
                    { 73, "Sumanjot", "Singh" },
                    { 74, "Simranjeet", "Singh" },
                    { 75, "Kanwarbir", "Singh" },
                    { 76, "Akshay", "Kumar" }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "TruckNumber", "TruckerId" },
                values: new object[,]
                {
                    { 1, "9362", 1 },
                    { 2, "9386", 2 },
                    { 3, "9394", 3 },
                    { 4, "9406", 3 },
                    { 5, "9412", 4 },
                    { 6, "9414", 5 },
                    { 7, "9416", 6 },
                    { 8, "9420", 5 },
                    { 9, "9422", 5 },
                    { 10, "9424", 7 },
                    { 11, "9430", 4 },
                    { 12, "9432", 4 },
                    { 13, "9436", 8 },
                    { 14, "9438", 9 },
                    { 15, "9440", 10 },
                    { 16, "9442", 9 },
                    { 17, "9446", 11 },
                    { 18, "9452", 12 },
                    { 19, "9460", 13 },
                    { 20, "9462", 14 },
                    { 21, "9464", 15 },
                    { 22, "9466", 4 },
                    { 23, "9468", 4 },
                    { 24, "9470", 4 },
                    { 25, "9472", 4 },
                    { 26, "9474", 16 },
                    { 27, "9476", 16 },
                    { 28, "9478", 17 },
                    { 29, "9480", 9 },
                    { 30, "9484", 18 },
                    { 31, "9486", 18 },
                    { 32, "9488", 5 },
                    { 33, "9490", 11 },
                    { 34, "9492", 2 },
                    { 35, "9494", 18 },
                    { 36, "9498", 19 },
                    { 37, "9500", 18 },
                    { 38, "9502", 20 },
                    { 39, "9504", 21 },
                    { 40, "9506", 4 },
                    { 41, "9508", 4 },
                    { 42, "9510", 4 },
                    { 43, "9512", 4 },
                    { 44, "9514", 2 },
                    { 45, "9516", 18 },
                    { 46, "9518", 21 },
                    { 47, "9522", 22 },
                    { 48, "9526", 23 },
                    { 49, "9534", 24 },
                    { 50, "9538", 2 },
                    { 51, "9544", 6 },
                    { 52, "9546", 18 },
                    { 53, "9548", 25 },
                    { 54, "9552", 26 },
                    { 55, "9554", 27 },
                    { 56, "9556", 22 },
                    { 57, "156", 5 },
                    { 58, "158", 28 },
                    { 59, "162", 5 },
                    { 60, "164", 5 },
                    { 61, "170", 29 },
                    { 62, "174", 3 },
                    { 63, "176", 3 },
                    { 64, "178", 3 },
                    { 65, "186", 3 },
                    { 66, "188", 3 },
                    { 67, "189", 30 },
                    { 68, "190", 3 },
                    { 69, "194", 31 },
                    { 70, "198", 32 },
                    { 71, "200", 33 },
                    { 72, "202", 33 },
                    { 73, "204", 34 },
                    { 74, "206", 35 },
                    { 75, "208", 36 },
                    { 76, "212", 3 },
                    { 77, "214", 37 },
                    { 78, "220", 38 },
                    { 79, "222", 3 },
                    { 80, "224", 3 },
                    { 81, "226", 33 },
                    { 82, "228", 39 },
                    { 83, "230", 33 },
                    { 84, "234", 39 },
                    { 85, "236", 39 },
                    { 86, "238", 40 },
                    { 87, "240", 5 },
                    { 88, "244", 15 },
                    { 89, "246", 36 },
                    { 90, "250", 4 },
                    { 91, "252", 4 },
                    { 92, "258", 41 },
                    { 93, "260", 42 },
                    { 94, "262", 43 },
                    { 95, "264", 36 },
                    { 96, "266", 15 },
                    { 97, "270", 15 },
                    { 98, "272", 15 },
                    { 99, "274", 15 },
                    { 100, "276", 33 },
                    { 101, "278", 33 },
                    { 102, "280", 33 },
                    { 103, "282", 3 },
                    { 104, "284", 21 },
                    { 105, "286", 44 },
                    { 106, "288", 4 },
                    { 107, "290", 4 },
                    { 108, "292", 4 },
                    { 109, "294", 45 },
                    { 110, "300", 16 },
                    { 111, "302", 16 },
                    { 112, "304", 15 },
                    { 113, "312", 46 },
                    { 114, "314", 47 },
                    { 115, "316", 48 },
                    { 116, "318", 49 },
                    { 117, "324", 50 },
                    { 118, "326", 39 },
                    { 119, "328", 16 },
                    { 120, "330", 16 },
                    { 121, "332", 16 },
                    { 122, "334", 51 },
                    { 123, "336", 16 },
                    { 124, "338", 39 },
                    { 125, "340", 52 },
                    { 126, "342", 41 },
                    { 127, "344", 39 },
                    { 128, "1320", 53 },
                    { 129, "1339", 54 },
                    { 130, "1416", 55 },
                    { 131, "1524", 56 },
                    { 132, "1550", 57 },
                    { 133, "1620", 58 },
                    { 134, "1628", 59 },
                    { 135, "1642", 60 },
                    { 136, "1646", 61 },
                    { 137, "1658", 62 },
                    { 138, "1660", 63 },
                    { 139, "1662", 64 },
                    { 140, "1664", 65 },
                    { 141, "1666", 65 },
                    { 142, "904150", 66 },
                    { 143, "907120", 67 },
                    { 144, "916136", 68 },
                    { 145, "919128", 69 },
                    { 146, "919134", 70 },
                    { 147, "919135", 69 },
                    { 148, "919137", 69 },
                    { 149, "919138", 69 },
                    { 150, "920127", 71 },
                    { 151, "920142", 72 },
                    { 152, "920145", 73 },
                    { 153, "920155", 74 },
                    { 154, "920157", 75 },
                    { 155, "921154", 76 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TruckerId",
                table: "Trucks",
                column: "TruckerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Truckers");
        }
    }
}
