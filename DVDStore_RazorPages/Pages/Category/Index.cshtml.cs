using DVDStore_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DVDStore_RazorPages.Models;

namespace DVDStore_RazorPages.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Models.Category> CategoryList { get; set; }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
