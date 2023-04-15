namespace Grp40_Assignment2.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public double StartingBidPrice { get; set; }
        public double BuyNowPrice { get; set; }
        public DateTime BidEnd { get;set; }
        public DateTime BidStart { get; set; }
        public string? highestBidder { get; set; }
        public string? Buyer { get; set; }
        public string Seller { get; set; } 
        public byte[]? ItemImage { get; set; }
    }
}
