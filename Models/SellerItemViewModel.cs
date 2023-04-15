namespace Grp40_Assignment2.Models
{
    public class SellerItemViewModel
    {
        public byte[] ProfilePicture { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IQueryable<Item> ItemList { get; set; }
        public IQueryable<UserReview> ReviewList { get; set; }
    }
}
