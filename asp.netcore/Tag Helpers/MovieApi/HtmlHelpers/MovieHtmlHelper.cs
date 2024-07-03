using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MovieApi.HtmlHelpers;

public static class MovieHtmlHelper
{
    public static IHtmlContent MovieLink(this IHtmlHelper htmlHelper)
    {
        string link = "<a href=\"https://google.com\">Google</a>";
        //string link = "<script>window.location.href = \"http://www.w3schools.com\"</script>";
        return new HtmlString(link);
    }

    public static IHtmlContent GoToDetail(this IHtmlHelper htmlHelper,string id)
    {
        var link = new TagBuilder("a");
        link.InnerHtml.Append("Deatils");
        link.Attributes.Add("href",$"https://localhost:8888/Home/Movie/{id}");
        link.AddCssClass("btn");
        link.AddCssClass("btn-primary");
        return link;
    }
}
