using DVDStore_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DVDStore_RazorPages.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Models.Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet() { }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "genre created successfully!";
                return RedirectToPage("/Category/Index");
            } else
            {
                return Page();
            }
        }

    }
}
