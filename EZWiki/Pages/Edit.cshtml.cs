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
using EZWiki.Helpers;

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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article =  await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
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

            // check if the slug already exists in the database.  
            var slug = UrlHelpers.URLFriendly(Article.Topic.ToLower());
            var isAvailable = !_context.Articles.Any(x => x.Slug == slug && x.Id != Article.Id);

            if (isAvailable == false)
            {
                ModelState.AddModelError($"{nameof(Article)}.{nameof(Article.Topic)}", $"{Article.Topic} already exists");
                return Page();
            }

            Article.Published = _clock.GetCurrentInstant();
            Article.Slug = slug;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect($"./{(Article.Slug == "home" ? "" : Article.Slug)}");
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
