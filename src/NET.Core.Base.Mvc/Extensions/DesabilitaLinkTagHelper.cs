using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NET.Core.Base.Mvc.Extensions
{
    [HtmlTargetElement("a", Attributes = "disabled-by-name")]
    [HtmlTargetElement("a", Attributes = "disabled-by-value")]
    public class DesabilitaLinkTagHelper : TagHelper
    {
        public DesabilitaLinkTagHelper(IHttpContextAccessor contextAcessor)
        {
            _contextAcessor = contextAcessor;
        }

        private readonly IHttpContextAccessor _contextAcessor;

        [HtmlAttributeName("disabled-by-name")]
        public string Name { get; set; }

        [HtmlAttributeName("disabled-by-value")]
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Aplicar validação
            var temAcesso = false;

            if (temAcesso) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "Você não tem permissão"));
        }
    }
}
