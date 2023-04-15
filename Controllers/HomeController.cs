using Grp40_Assignment2.Data;
using Grp40_Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace Grp40_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, 
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string searchString, string category)
        {
            ViewBag.ItemCategory = _context.Categories
                                  .OrderBy(c => c.Name).ToList();

            IQueryable<Item> items = _context.Items;

            // Apply search filter if search string is not null or empty
            if (!String.IsNullOrEmpty(searchString))
            {
                // Persist the selected category in the search form
                ViewBag.SearchString = searchString;
                ViewBag.Category = category;

                items = items.Where(i => i.Name.Contains(searchString) || i.Seller.Contains(searchString));
            }
            else
            {
                ViewBag.SearchString = "";
                ViewBag.Category = category;
            }

            // Apply category filter if category is not null or empty
            if (!String.IsNullOrEmpty(category))
            {
                var categoryId = _context.Categories
                                    .Where(c => c.Name == category)
                                    .Select(c => c.CategoryId)
                                    .FirstOrDefault();

                items = items.Where(i => i.CategoryId == categoryId);
            }

            var itemImages = new Dictionary<Item, string>();
            foreach (var item in items)
            {
                if (item.ItemImage != null)
                {
                    itemImages.Add(item, Convert.ToBase64String(item.ItemImage));
                }
                else
                {
                    itemImages.Add(item, null);
                }
            }
            ViewBag.ItemImages = itemImages;

            return View(items.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var items = _context.Items.Where(i => i.Seller == username);

            var reviews = _context.UserReviews.Where(r => r.seller == username);

            // calculate the average rating of the profile

            var sumReviews = _context.UserReviews.Where(r => r.seller == username).Sum(r => r.rating);
            var countReviews = _context.UserReviews.Count(r => r.seller == username);

            var averageRating = 0;
            if (countReviews  > 0) {
                averageRating = sumReviews / countReviews;
                ViewBag.AverageRating = string.Format("{0:F1}", averageRating);
            }
            else
            {
                ViewBag.AverageRating = "This seller has no reviews";
            }            

            var sellerItemViewModel = new SellerItemViewModel();

            sellerItemViewModel.ProfilePicture = user.ProfilePicture;
            sellerItemViewModel.UserId = user.Id;
            sellerItemViewModel.UserName = user.UserName;
            sellerItemViewModel.FirstName = user.FirstName;
            sellerItemViewModel.LastName = user.LastName;
            sellerItemViewModel.Email = user.Email;
            sellerItemViewModel.PhoneNumber = user.PhoneNumber;
            sellerItemViewModel.ItemList = items;
            sellerItemViewModel.ReviewList = reviews;

            return View(sellerItemViewModel);
        }
        
        
        [HttpPost]
        [Authorize(Roles ="Buyer, Seller")]
        public async Task<IActionResult> AddReview()
        {
            var rev = new UserReview();

            rev.reviewer = User.Identity.Name;
            rev.seller = Request.Form["seller"];
            rev.feedback = Request.Form["feedback"];
            rev.rating = int.Parse(Request.Form["rating"]);
            
            _context.Add(rev);
            _context.SaveChanges();

            return RedirectToAction("Profile", new { username = rev.seller });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}