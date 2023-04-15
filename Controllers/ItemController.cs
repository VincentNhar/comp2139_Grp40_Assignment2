using Grp40_Assignment2.Data;
using Grp40_Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grp40_Assignment2.Controllers
{
    [Authorize(Roles = "Buyer, Seller")]
    public class ItemController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly ApplicationDbContext _context;

        public ItemController(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string username)
        {
            ViewBag.Action = "Sell Item";
            ViewBag.ItemCategory = _context.Categories
                          .OrderBy(c => c.Name).ToList();

            // retrieves user's information from the database
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with username = {username} cannot be found";
                return View("Edit");
            }

            ViewBag.User = user;

            return View("Edit", new Item());
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            ViewBag.ItemCategory = _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new { Id = c.CategoryId, Name = c.Name })
                .ToList();

            IQueryable<Item> items = _context.Items.Where(i => i.Seller == user.UserName);

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
        public async Task<IActionResult> Edit(int itemId)
        {
            // retrieves logged in user's information from the database
            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewBag.User = user.UserName;

            ViewBag.Action = "Edit Item";
            ViewBag.ItemCategory = _context.Categories
                          .OrderBy(c => c.Name).ToList();

            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {

            if (ModelState.IsValid)
            {
                string action = (item.Seller == null) ? "Add" : "Edit";

                // grabs the value from the form stored in name attribute
                string bidEndValue = Request.Form["btnradio"];

                item.BidEnd = item.BidStart.AddDays(Convert.ToDouble(bidEndValue));

                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        item.ItemImage = dataStream.ToArray();
                    }
                }

                if (action == "Add")
                {
                    _context.Add(item);
                }
                else
                {
                    _context.Update(item);
                }
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ItemCategory = _context.Categories
                          .OrderBy(c => c.Name).ToList();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            string strDelItemId = Request.Form["deleteId"];
            // retrieves the value and store it as an int
            int intDelItemId = int.Parse(strDelItemId);

            // retrieves the item from the DB based on the retrieved ID from the view
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == intDelItemId);

            // remove the item from the DB
            _context.Items.Remove(item);

            // apply changes
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id)
        {
            ViewBag.Action = "Product Details";
            var item = _context.Items.FirstOrDefault(i => i.ItemId == id);
            var itemImage = new Dictionary<Item, string>();
            if (item.ItemImage != null)
            {
                itemImage.Add(item, Convert.ToBase64String(item.ItemImage));
            }
            else
            {
                itemImage.Add(item, null);
            }
            ViewBag.ItemImage = itemImage;
            return View(item);
        }
    }
}
