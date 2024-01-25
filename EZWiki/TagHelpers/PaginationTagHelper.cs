using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EZWiki.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("Pagination")]
    public class PaginationTagHelper : TagHelper
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        private readonly IHtmlGenerator _htmlGenerator;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string AspPage { get; set; }

        public PaginationTagHelper(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";

            if (context.AllAttributes["class"] != null)
            {
                output.Attributes.SetAttribute("class", "pagination " + context.AllAttributes["class"].Value);
            }
            else
            {
                output.Attributes.SetAttribute("class", "pagination");
            }

            for (int pageNum = 1; pageNum <= TotalPages; pageNum++)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("page-item");
                if (pageNum == PageNumber)
                {
                    li.AddCssClass("active");
                    TagBuilder span = new TagBuilder("span");
                    span.AddCssClass("page-link");
                    span.InnerHtml.Append($"{pageNum}");
                    li.InnerHtml.AppendHtml(span);
                }
                else
                {
                    object route = new { PageNumber = pageNum };
                    TagBuilder a = _htmlGenerator.GeneratePageLink(
                        ViewContext,
                        linkText: pageNum.ToString(),
                        pageName: AspPage,
                        pageHandler: string.Empty,
                        protocol: string.Empty,
                        hostname: string.Empty,
                        fragment: string.Empty,
                        routeValues: route,
                        htmlAttributes: null
                    );
                    a.AddCssClass("page-link");
                    li.InnerHtml.AppendHtml(a);
                }
                output.Content.AppendHtml(li);
            }
            await base.ProcessAsync(context, output);
            return;
        }

        
    }
}
