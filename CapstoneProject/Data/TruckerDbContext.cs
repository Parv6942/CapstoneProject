using CapstoneProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CapstoneProject.Data
{
    public class TruckerDbContext : DbContext
    {
        public DbSet<Trucker> Truckers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }


        public TruckerDbContext(DbContextOptions<TruckerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Trucker)
                .WithMany(tr => tr.Transactions)
                .HasForeignKey(t => t.TruckerId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Item)
                .WithMany()
                .HasForeignKey(t => t.ItemId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Trucker)
                .WithMany()
                .HasForeignKey(o => o.TruckerId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany()
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Order)
                .WithMany()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Trucker)
                .WithMany()
                .HasForeignKey(i => i.TruckerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Truck>()
             .HasOne(t => t.Trucker)
             .WithMany(tr => tr.Trucks)
             .HasForeignKey(t => t.TruckerId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trucker>().HasData(
                new Trucker { Id = 1, FirstName = "Kuljeet", LastName = "Singh Sidhu" },
                new Trucker { Id = 2, FirstName = "Gurpreet", LastName = "Singh Kooner" },
                new Trucker { Id = 3, FirstName = "Luis", LastName = "S Azevedo" },
                new Trucker { Id = 4, FirstName = "Ravinder", LastName = "Jaswal" },
                new Trucker { Id = 5, FirstName = "Yadwinder", LastName = "Singh" },
                new Trucker { Id = 6, FirstName = "Manjinder", LastName = "Singh" },
                new Trucker { Id = 7, FirstName = "Jatinder", LastName = "Singh" },
                new Trucker { Id = 8, FirstName = "Preetinder", LastName = "Singh" },
                new Trucker { Id = 9, FirstName = "Tarinder", LastName = "Singh Dosanjh" },
                new Trucker { Id = 10, FirstName = "Balwinder", LastName = "Singh Sandhu" },
                new Trucker { Id = 11, FirstName = "Jasmeet", LastName = "Singh Grewal" },
                new Trucker { Id = 12, FirstName = "Simratpal", LastName = "Singh" },
                new Trucker { Id = 13, FirstName = "Harjinder", LastName = "Singh" },
                new Trucker { Id = 14, FirstName = "Sidhant", LastName = "Sharda" },
                new Trucker { Id = 15, FirstName = "Chetanbir", LastName = "Sohal" },
                new Trucker { Id = 16, FirstName = "Hardeep", LastName = "Singh" },
                new Trucker { Id = 17, FirstName = "Lawan", LastName = "Kapoor" },
                new Trucker { Id = 18, FirstName = "Balkaran", LastName = "Dhillon" },
                new Trucker { Id = 19, FirstName = "Amanjot", LastName = "Singh" },
                new Trucker { Id = 20, FirstName = "Kuljit", LastName = "Singh ." },
                new Trucker { Id = 21, FirstName = "Gurpreet", LastName = "Singh Dhillon" },
                new Trucker { Id = 22, FirstName = "Akashdeep", LastName = "Singh" },
                new Trucker { Id = 23, FirstName = "Roop Kamal", LastName = "Singh Mand" },
                new Trucker { Id = 24, FirstName = "Harpreet", LastName = "Singh" },
                new Trucker { Id = 25, FirstName = "Vikramjit", LastName = "Singh Sandhu" },
                new Trucker { Id = 26, FirstName = "Jobanpreet", LastName = "Singh" },
                new Trucker { Id = 27, FirstName = "Navpreet", LastName = "Singh Randhay" },
                new Trucker { Id = 28, FirstName = "Jatinder", LastName = "Singh Bains" },
                new Trucker { Id = 29, FirstName = "Paramjit", LastName = "Singh Ghuman" },
                new Trucker { Id = 30, FirstName = "Chamkaur", LastName = "Singh Kaile" },
                new Trucker { Id = 31, FirstName = "Sukhpreet", LastName = "Singh" },
                new Trucker { Id = 32, FirstName = "Harcharan", LastName = "Singh" },
                new Trucker { Id = 33, FirstName = "Kelly", LastName = "Fagundes Giampapa" },
                new Trucker { Id = 34, FirstName = "Harmanpreet", LastName = "Bajwa" },
                new Trucker { Id = 35, FirstName = "Jaspal", LastName = "Gill" },
                new Trucker { Id = 36, FirstName = "Jaskarn", LastName = "Singh" },
                new Trucker { Id = 37, FirstName = "Rajender", LastName = "Singh Dhesi" },
                new Trucker { Id = 38, FirstName = "Sami", LastName = "Mohamed Gharbiya" },
                new Trucker { Id = 39, FirstName = "Pardeep", LastName = "Singh Grewal" },
                new Trucker { Id = 40, FirstName = "Lovepreet", LastName = "Singh" },
                new Trucker { Id = 41, FirstName = "Manveer", LastName = "Singh" },
                new Trucker { Id = 42, FirstName = "Jagmeet", LastName = "Singh" },
                new Trucker { Id = 43, FirstName = "Gurjit", LastName = "Singh Chahal" },
                new Trucker { Id = 44, FirstName = "Pawanpreet", LastName = "Singh" },
                new Trucker { Id = 45, FirstName = "Gurvinder", LastName = "Pannu" },
                new Trucker { Id = 46, FirstName = "Satnam", LastName = "Singh Saini" },
                new Trucker { Id = 47, FirstName = "Hardeep", LastName = "Singh Dhaliwal" },
                new Trucker { Id = 48, FirstName = "Kulwinder", LastName = "Singh Bagga" },
                new Trucker { Id = 49, FirstName = "Amrik", LastName = "Singh" },
                new Trucker { Id = 50, FirstName = "Karminder", LastName = "Singh Cheema" },
                new Trucker { Id = 51, FirstName = "Himmat", LastName = "Singh" },
                new Trucker { Id = 52, FirstName = "Balraj", LastName = "Singh Gill" },
                new Trucker { Id = 53, FirstName = "Bahadur", LastName = "Singh Thiara" },
                new Trucker { Id = 54, FirstName = "Rajvir", LastName = "Singh Sagi" },
                new Trucker { Id = 55, FirstName = "Hardeep 1416", LastName = "Singh" },
                new Trucker { Id = 56, FirstName = "Jaskirandeep", LastName = "Singh Gill" },
                new Trucker { Id = 57, FirstName = "Chaudhery Amir", LastName = "Amin Bajwa" },
                new Trucker { Id = 58, FirstName = "Gurvinder", LastName = "Singh Kahlon" },
                new Trucker { Id = 59, FirstName = "Kulwant", LastName = "Sandhu" },
                new Trucker { Id = 60, FirstName = "Gurbhej", LastName = "Singh Kahlon" },
                new Trucker { Id = 61, FirstName = "Gurpreet", LastName = "Singh Shergill" },
                new Trucker { Id = 62, FirstName = "Jan", LastName = "Wyzga" },
                new Trucker { Id = 63, FirstName = "Mervin Walton", LastName = "Robinson" },
                new Trucker { Id = 64, FirstName = "Jun", LastName = "Zhu" },
                new Trucker { Id = 65, FirstName = "Kulwant Singh", LastName = "Sandhu" },
                new Trucker { Id = 66, FirstName = "Radhe Sham", LastName = "Thind" },
                new Trucker { Id = 67, FirstName = "Shahid", LastName = "Rashid" },
                new Trucker { Id = 68, FirstName = "Kanwaljot Singh", LastName = "Sandhu" },
                new Trucker { Id = 69, FirstName = "Jaspal", LastName = "Brar" },
                new Trucker { Id = 70, FirstName = "Ishneet Paul", LastName = "Singh Sandhu" },
                new Trucker { Id = 71, FirstName = "Zauhar Alankar", LastName = "Gill" },
                new Trucker { Id = 72, FirstName = "Kapil Kumar", LastName = "Muttan" },
                new Trucker { Id = 73, FirstName = "Sumanjot", LastName = "Singh" },
                new Trucker { Id = 74, FirstName = "Simranjeet", LastName = "Singh" },
                new Trucker { Id = 75, FirstName = "Kanwarbir", LastName = "Singh" },
                new Trucker { Id = 76, FirstName = "Akshay", LastName = "Kumar" }
            );


            modelBuilder.Entity<AdminUser>().HasData(
                new AdminUser { Id = 1, Username = "Admin", Password = "Password123", IsTopAdmin = true }
            );


            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Tire", Price = 120.00m, ImageUrl = "item1.jpg" },
                new Item { Id = 2, Name = "Oil", Price = 40.00m, ImageUrl = "item2.jpeg" },
                new Item { Id = 3, Name = "Brake Pads", Price = 70.00m, ImageUrl = "item3.jpg" },
                new Item { Id = 4, Name = "Wiper Blades", Price = 25.00m, ImageUrl = "item4.jpg" }
            );
            modelBuilder.Entity<Truck>().HasData(
               new Truck { Id = 1, TruckNumber = "9362", TruckerId = 1 },
               new Truck { Id = 2, TruckNumber = "9386", TruckerId = 2 },
               new Truck { Id = 3, TruckNumber = "9394", TruckerId = 3 },
               new Truck { Id = 4, TruckNumber = "9406", TruckerId = 3 },
               new Truck { Id = 5, TruckNumber = "9412", TruckerId = 4 },
               new Truck { Id = 6, TruckNumber = "9414", TruckerId = 5 },
               new Truck { Id = 7, TruckNumber = "9416", TruckerId = 6 },
               new Truck { Id = 8, TruckNumber = "9420", TruckerId = 5 },
               new Truck { Id = 9, TruckNumber = "9422", TruckerId = 5 },
               new Truck { Id = 10, TruckNumber = "9424", TruckerId = 7 },
               new Truck { Id = 11, TruckNumber = "9430", TruckerId = 4 },
               new Truck { Id = 12, TruckNumber = "9432", TruckerId = 4 },
               new Truck { Id = 13, TruckNumber = "9436", TruckerId = 8 },
               new Truck { Id = 14, TruckNumber = "9438", TruckerId = 9 },
               new Truck { Id = 15, TruckNumber = "9440", TruckerId = 10 },
               new Truck { Id = 16, TruckNumber = "9442", TruckerId = 9 },
               new Truck { Id = 17, TruckNumber = "9446", TruckerId = 11 },
               new Truck { Id = 18, TruckNumber = "9452", TruckerId = 12 },
               new Truck { Id = 19, TruckNumber = "9460", TruckerId = 13 },
               new Truck { Id = 20, TruckNumber = "9462", TruckerId = 14 },
               new Truck { Id = 21, TruckNumber = "9464", TruckerId = 15 },
               new Truck { Id = 22, TruckNumber = "9466", TruckerId = 4 },
               new Truck { Id = 23, TruckNumber = "9468", TruckerId = 4 },
               new Truck { Id = 24, TruckNumber = "9470", TruckerId = 4 },
               new Truck { Id = 25, TruckNumber = "9472", TruckerId = 4 },
               new Truck { Id = 26, TruckNumber = "9474", TruckerId = 16 },
               new Truck { Id = 27, TruckNumber = "9476", TruckerId = 16 },
               new Truck { Id = 28, TruckNumber = "9478", TruckerId = 17 },
               new Truck { Id = 29, TruckNumber = "9480", TruckerId = 9 },
               new Truck { Id = 30, TruckNumber = "9484", TruckerId = 18 },
               new Truck { Id = 31, TruckNumber = "9486", TruckerId = 18 },
               new Truck { Id = 32, TruckNumber = "9488", TruckerId = 5 },
               new Truck { Id = 33, TruckNumber = "9490", TruckerId = 11 },
               new Truck { Id = 34, TruckNumber = "9492", TruckerId = 2 },
               new Truck { Id = 35, TruckNumber = "9494", TruckerId = 18 },
               new Truck { Id = 36, TruckNumber = "9498", TruckerId = 19 },
               new Truck { Id = 37, TruckNumber = "9500", TruckerId = 18 },
               new Truck { Id = 38, TruckNumber = "9502", TruckerId = 20 },
               new Truck { Id = 39, TruckNumber = "9504", TruckerId = 21 },
               new Truck { Id = 40, TruckNumber = "9506", TruckerId = 4 },
               new Truck { Id = 41, TruckNumber = "9508", TruckerId = 4 },
               new Truck { Id = 42, TruckNumber = "9510", TruckerId = 4 },
               new Truck { Id = 43, TruckNumber = "9512", TruckerId = 4 },
               new Truck { Id = 44, TruckNumber = "9514", TruckerId = 2 },
               new Truck { Id = 45, TruckNumber = "9516", TruckerId = 18 },
               new Truck { Id = 46, TruckNumber = "9518", TruckerId = 21 },
               new Truck { Id = 47, TruckNumber = "9522", TruckerId = 22 },
               new Truck { Id = 48, TruckNumber = "9526", TruckerId = 23 },
               new Truck { Id = 49, TruckNumber = "9534", TruckerId = 24 },
               new Truck { Id = 50, TruckNumber = "9538", TruckerId = 2 },
               new Truck { Id = 51, TruckNumber = "9544", TruckerId = 6 },
               new Truck { Id = 52, TruckNumber = "9546", TruckerId = 18 },
               new Truck { Id = 53, TruckNumber = "9548", TruckerId = 25 },
               new Truck { Id = 54, TruckNumber = "9552", TruckerId = 26 },
               new Truck { Id = 55, TruckNumber = "9554", TruckerId = 27 },
               new Truck { Id = 56, TruckNumber = "9556", TruckerId = 22 },
               new Truck { Id = 57, TruckNumber = "156", TruckerId = 5 },
               new Truck { Id = 58, TruckNumber = "158", TruckerId = 28 },
               new Truck { Id = 59, TruckNumber = "162", TruckerId = 5 },
               new Truck { Id = 60, TruckNumber = "164", TruckerId = 5 },
               new Truck { Id = 61, TruckNumber = "170", TruckerId = 29 },
               new Truck { Id = 62, TruckNumber = "174", TruckerId = 3 },
               new Truck { Id = 63, TruckNumber = "176", TruckerId = 3 },
               new Truck { Id = 64, TruckNumber = "178", TruckerId = 3 },
               new Truck { Id = 65, TruckNumber = "186", TruckerId = 3 },
               new Truck { Id = 66, TruckNumber = "188", TruckerId = 3 },
               new Truck { Id = 67, TruckNumber = "189", TruckerId = 30 },
               new Truck { Id = 68, TruckNumber = "190", TruckerId = 3 },
               new Truck { Id = 69, TruckNumber = "194", TruckerId = 31 },
               new Truck { Id = 70, TruckNumber = "198", TruckerId = 32 },
               new Truck { Id = 71, TruckNumber = "200", TruckerId = 33 },
               new Truck { Id = 72, TruckNumber = "202", TruckerId = 33 },
               new Truck { Id = 73, TruckNumber = "204", TruckerId = 34 },
               new Truck { Id = 74, TruckNumber = "206", TruckerId = 35 },
               new Truck { Id = 75, TruckNumber = "208", TruckerId = 36 },
               new Truck { Id = 76, TruckNumber = "212", TruckerId = 3 },
               new Truck { Id = 77, TruckNumber = "214", TruckerId = 37 },
               new Truck { Id = 78, TruckNumber = "220", TruckerId = 38 },
               new Truck { Id = 79, TruckNumber = "222", TruckerId = 3 },
               new Truck { Id = 80, TruckNumber = "224", TruckerId = 3 },
               new Truck { Id = 81, TruckNumber = "226", TruckerId = 33 },
               new Truck { Id = 82, TruckNumber = "228", TruckerId = 39 },
               new Truck { Id = 83, TruckNumber = "230", TruckerId = 33 },
               new Truck { Id = 84, TruckNumber = "234", TruckerId = 39 },
               new Truck { Id = 85, TruckNumber = "236", TruckerId = 39 },
               new Truck { Id = 86, TruckNumber = "238", TruckerId = 40 },
               new Truck { Id = 87, TruckNumber = "240", TruckerId = 5 },
               new Truck { Id = 88, TruckNumber = "244", TruckerId = 15 },
               new Truck { Id = 89, TruckNumber = "246", TruckerId = 36 },
               new Truck { Id = 90, TruckNumber = "250", TruckerId = 4 },
               new Truck { Id = 91, TruckNumber = "252", TruckerId = 4 },
               new Truck { Id = 92, TruckNumber = "258", TruckerId = 41 },
               new Truck { Id = 93, TruckNumber = "260", TruckerId = 42 },
               new Truck { Id = 94, TruckNumber = "262", TruckerId = 43 },
               new Truck { Id = 95, TruckNumber = "264", TruckerId = 36 },
               new Truck { Id = 96, TruckNumber = "266", TruckerId = 15 },
               new Truck { Id = 97, TruckNumber = "270", TruckerId = 15 },
               new Truck { Id = 98, TruckNumber = "272", TruckerId = 15 },
               new Truck { Id = 99, TruckNumber = "274", TruckerId = 15 },
               new Truck { Id = 100, TruckNumber = "276", TruckerId = 33 },
               new Truck { Id = 101, TruckNumber = "278", TruckerId = 33 },
               new Truck { Id = 102, TruckNumber = "280", TruckerId = 33 },
               new Truck { Id = 103, TruckNumber = "282", TruckerId = 3 },
               new Truck { Id = 104, TruckNumber = "284", TruckerId = 21 },
               new Truck { Id = 105, TruckNumber = "286", TruckerId = 44 },
               new Truck { Id = 106, TruckNumber = "288", TruckerId = 4 },
               new Truck { Id = 107, TruckNumber = "290", TruckerId = 4 },
               new Truck { Id = 108, TruckNumber = "292", TruckerId = 4 },
               new Truck { Id = 109, TruckNumber = "294", TruckerId = 45 },
               new Truck { Id = 110, TruckNumber = "300", TruckerId = 16 },
               new Truck { Id = 111, TruckNumber = "302", TruckerId = 16 },
               new Truck { Id = 112, TruckNumber = "304", TruckerId = 15 },
               new Truck { Id = 113, TruckNumber = "312", TruckerId = 46 },
               new Truck { Id = 114, TruckNumber = "314", TruckerId = 47 },
               new Truck { Id = 115, TruckNumber = "316", TruckerId = 48 },
               new Truck { Id = 116, TruckNumber = "318", TruckerId = 49 },
               new Truck { Id = 117, TruckNumber = "324", TruckerId = 50 },
               new Truck { Id = 118, TruckNumber = "326", TruckerId = 39 },
               new Truck { Id = 119, TruckNumber = "328", TruckerId = 16 },
               new Truck { Id = 120, TruckNumber = "330", TruckerId = 16 },
               new Truck { Id = 121, TruckNumber = "332", TruckerId = 16 },
               new Truck { Id = 122, TruckNumber = "334", TruckerId = 51 },
               new Truck { Id = 123, TruckNumber = "336", TruckerId = 16 },
               new Truck { Id = 124, TruckNumber = "338", TruckerId = 39 },
               new Truck { Id = 125, TruckNumber = "340", TruckerId = 52 },
               new Truck { Id = 126, TruckNumber = "342", TruckerId = 41 },
               new Truck { Id = 127, TruckNumber = "344", TruckerId = 39 },
               new Truck { Id = 128, TruckNumber = "1320", TruckerId = 53 },
               new Truck { Id = 129, TruckNumber = "1339", TruckerId = 54 },
               new Truck { Id = 130, TruckNumber = "1416", TruckerId = 55 },
               new Truck { Id = 131, TruckNumber = "1524", TruckerId = 56 },
               new Truck { Id = 132, TruckNumber = "1550", TruckerId = 57 },
               new Truck { Id = 133, TruckNumber = "1620", TruckerId = 58 },
               new Truck { Id = 134, TruckNumber = "1628", TruckerId = 59 },
               new Truck { Id = 135, TruckNumber = "1642", TruckerId = 60 },
               new Truck { Id = 136, TruckNumber = "1646", TruckerId = 61 },
               new Truck { Id = 137, TruckNumber = "1658", TruckerId = 62 },
               new Truck { Id = 138, TruckNumber = "1660", TruckerId = 63 },
               new Truck { Id = 139, TruckNumber = "1662", TruckerId = 64 },
               new Truck { Id = 140, TruckNumber = "1664", TruckerId = 65 },
               new Truck { Id = 141, TruckNumber = "1666", TruckerId = 65 },
               new Truck { Id = 142, TruckNumber = "904150", TruckerId = 66 },
               new Truck { Id = 143, TruckNumber = "907120", TruckerId = 67 },
               new Truck { Id = 144, TruckNumber = "916136", TruckerId = 68 },
               new Truck { Id = 145, TruckNumber = "919128", TruckerId = 69 },
               new Truck { Id = 146, TruckNumber = "919134", TruckerId = 70 },
               new Truck { Id = 147, TruckNumber = "919135", TruckerId = 69 },
               new Truck { Id = 148, TruckNumber = "919137", TruckerId = 69 },
               new Truck { Id = 149, TruckNumber = "919138", TruckerId = 69 },
               new Truck { Id = 150, TruckNumber = "920127", TruckerId = 71 },
               new Truck { Id = 151, TruckNumber = "920142", TruckerId = 72 },
               new Truck { Id = 152, TruckNumber = "920145", TruckerId = 73 },
               new Truck { Id = 153, TruckNumber = "920155", TruckerId = 74 },
               new Truck { Id = 154, TruckNumber = "920157", TruckerId = 75 },
               new Truck { Id = 155, TruckNumber = "921154", TruckerId = 76 }
           );
        }
    }
}


