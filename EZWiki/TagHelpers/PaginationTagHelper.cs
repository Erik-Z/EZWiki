using Microsoft.AspNetCore.Mvc.Rendering;
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

        public override void Process(TagHelperContext context, TagHelperOutput output)
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
                TagBuilder a = new TagBuilder("a");
                
                a.Attributes.Add("class", "page-link");
                a.Attributes.Add("href", $"?PageNumber={pageNum}");
                a.InnerHtml.Append(pageNum.ToString());

                li.InnerHtml.AppendHtml(a);
                li.AddCssClass("page-item");
                if (pageNum == PageNumber)
                {
                    li.AddCssClass("active");
                }
                output.Content.AppendHtml(li);
            }
        }

        
    }
}
