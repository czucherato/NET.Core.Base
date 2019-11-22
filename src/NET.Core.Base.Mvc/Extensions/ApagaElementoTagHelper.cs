using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NET.Core.Base.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "supress-by-name")]
    [HtmlTargetElement("*", Attributes = "supress-by-value")]
    public class ApagaElementoTagHelper : TagHelper
    {
        public ApagaElementoTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private readonly IHttpContextAccessor _contextAccessor;

        [HtmlAttributeName("supress-by-name")]
        public string Name { get; set; }

        [HtmlAttributeName("supress-by-value")]
        public string Value { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentException(nameof(context));
            if (output == null) throw new ArgumentException(nameof(output));

            //Implementar validação
            var temAcesso = false;

            if (temAcesso) return;

            output.SuppressOutput();
        }
    }
}
