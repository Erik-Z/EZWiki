using EZWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EZWiki.Pages
{
    public class AllArticlesModel : PageModel
    {
        public readonly ApplicationDbContext _context;
        public const int _PageSize = 1;

        public AllArticlesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [FromQuery]
        public int PageNumber { get; set; } = 1;
        public int TotalPages => (_context.Articles.Count() + _PageSize - 1) / _PageSize;
        public IEnumerable<Article> Articles { get; set; }

        public async Task OnGetAsync()
        {
            Articles = await _context.Articles
                .AsNoTracking()
                .OrderBy(a => a.Topic)
                .Skip((PageNumber - 1) * _PageSize)
                .Take(_PageSize)
                .ToArrayAsync();
                
        }
    }
}
