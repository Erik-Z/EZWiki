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
    public class DetailsModel : PageModel
    {
        private readonly EZWiki.Models.ApplicationDbContext _context;

        public DetailsModel(EZWiki.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            id = id ?? "Home";
            
            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Topic == id);
           
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                Article = article;
            }
            return Page();
        }
    }
}
