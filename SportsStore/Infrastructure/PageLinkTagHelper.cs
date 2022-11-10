using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlFactory)
        {
            this.urlFactory = urlFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        // passed in
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnable { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("div");
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                if (PageClassesEnable)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
