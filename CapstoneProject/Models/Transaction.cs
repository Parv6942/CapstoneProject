namespace CapstoneProject.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int TruckerId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Trucker Trucker { get; set; }
        public virtual Item Item { get; set; }
    }
}
