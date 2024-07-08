using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MovieApi.Extensions;

[HtmlTargetElement("img",Attributes = "error-src",TagStructure = TagStructure.WithoutEndTag)]
public class ImgErrorTagHelper : TagHelper
{
    public string ErrorSrc { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Add("onerror",$"event.target.src='{ErrorSrc}'");
    }
}
