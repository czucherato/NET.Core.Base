using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NET.Core.Base.Mvc.Extensions
{
    [HtmlTargetElement("*", Attributes = "suppress-by-action")]
    public class ApagaElementoByActionTagHelper : TagHelper
    {
        public ApagaElementoByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private readonly IHttpContextAccessor _contextAccessor;


        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentException(nameof(context));
            if (output == null) throw new ArgumentException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}
