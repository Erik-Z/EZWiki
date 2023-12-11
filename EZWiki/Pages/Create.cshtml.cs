using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodaTime;
using EZWiki.Models;

namespace EZWiki.Pages
{
    public class CreateModel : PageModel
    {
        private readonly EZWiki.Models.ApplicationDbContext _context;
        private readonly IClock _clock;

        public CreateModel(EZWiki.Models.ApplicationDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Article Article { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article.Published = _clock.GetCurrentInstant();

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return Redirect($"./{Article.Topic}");
        }
    }
}
