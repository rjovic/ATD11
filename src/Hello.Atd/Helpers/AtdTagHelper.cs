using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace Hello.Atd.Helpers
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElementAttribute("atd-tag")]
    public class AtdTagHelper : TagHelper
    {
        [HtmlAttributeName("message")]
        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Clear();
            output.Content.SetContent(Message);
        }
    }
}
