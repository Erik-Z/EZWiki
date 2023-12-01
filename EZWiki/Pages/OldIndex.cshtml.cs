using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EZWiki.Models;

namespace EZWiki.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EZWiki.Models.ApplicationDbContext _context;

        public IndexModel(EZWiki.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
