using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using EZWiki.Models;

namespace EZWiki.Pages
{
    public class EditModel : PageModel
    {
        private readonly EZWiki.Models.ApplicationDbContext _context;
        private readonly IClock _clock;

        public EditModel(EZWiki.Models.ApplicationDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =  await _context.Articles.FirstOrDefaultAsync(m => m.Topic == id);
            if (article == null)
            {
                return NotFound();
            }
            Article = article;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Article).State = EntityState.Modified;
            Article.Published = _clock.GetCurrentInstant();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Topic))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect($"./{(Article.Topic == "HomePage" ? "" : Article.Topic)}");
        }

        private bool ArticleExists(string id)
        {
            return _context.Articles.Any(e => e.Topic == id);
        }
    }
}
