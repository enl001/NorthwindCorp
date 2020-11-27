using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindCorp.Web.TagHelpers
{
  [HtmlTargetElement(Attributes = "northwind-id")]
  public class NorthwindImageTagHelper: TagHelper
  {
    public int NorthwindId { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.Attributes.SetAttribute("href", $"/Categories/{NorthwindId}/image");
      output.Attributes.SetAttribute("target", "_blank");
    }
  }
}