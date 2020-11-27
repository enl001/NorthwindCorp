using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace NorthwindCorp.Web.HtmlHelpers
{
  public static class NorthwindImageLinkHelper
  {
    public static HtmlString NorthwindImageLink(this IHtmlHelper html, int id, string content)
    {
      TagBuilder a = new TagBuilder("a");
      a.Attributes.Add("href", $"/Categories/{id}/image");
      a.Attributes.Add("target", "_blank");
      a.InnerHtml.SetContent(content);
      var writer = new System.IO.StringWriter();
      a.WriteTo(writer, HtmlEncoder.Default);
      return new HtmlString(writer.ToString());
    }
  }
}
