using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModel;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }
        public PagingInfo? PageModel { get; set; }
        public string? PageAction {  get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = string.Empty;
        public string PageClassNormal { get; set; } = string.Empty;
        public string PageClassSelected { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("div");
                for (int i = 1; i <= PageModel.TotalPage; i++)
                {
                    TagBuilder a = new TagBuilder("a");
                    PageUrlValues["productPage"] = i;
                    a.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    if (PageClassesEnabled)
                    {
                        a.AddCssClass(PageClass);
                        a.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                    }
                    a.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(a);

                    
                }
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
