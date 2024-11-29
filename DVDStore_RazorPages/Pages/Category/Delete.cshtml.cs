using DVDStore_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DVDStore_RazorPages.Pages.Category
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Models.Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            Category = _db.Categories.FirstOrDefault(Category => Category.Id == id);
            if (Category is null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPost() 
        {
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            return RedirectToPage("/Category/Index");
        }
    }
}
