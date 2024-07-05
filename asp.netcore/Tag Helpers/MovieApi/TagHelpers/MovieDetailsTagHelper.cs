using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MovieApi.Models;
using System.Text.Encodings.Web;

namespace MovieApi.TagHelpers;

[HtmlTargetElement("a",Attributes = "movie-model")]
public class MovieDetailsTagHelper : TagHelper
{
    private readonly IUrlHelperFactory urlHelperFactory;

    public MovieDetailsTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        this.urlHelperFactory = urlHelperFactory;
    }
    public Movie MovieModel { get; set; }

    [ViewContext]
    public ViewContext Context { get; set; }

    public override void Process(TagHelperContext context,
        TagHelperOutput output)
    {
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(Context);

        output.TagName = "a";
        output.AddClass("btn",HtmlEncoder.Default);
        output.AddClass("btn-primary",HtmlEncoder.Default);
        output.Attributes.Add("title", MovieModel.Title);

        string url = urlHelper.ActionLink("Movie","Home",new { id = MovieModel.imdbID});

        var icon = new TagBuilder("i");
        if (MovieModel.Type == "movie")
            icon.AddCssClass("fa fa-film");
        if (MovieModel.Type == "game")
            icon.AddCssClass("fa fa-gamepad");
        else
            icon.AddCssClass("fa fa-film");
        output.Content.AppendHtml(icon);
        output.Content.Append("  Details");
        output.Attributes.Add("href",url);
    }
}
