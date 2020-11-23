using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace NorthwindCorp.Web.Models
{
  public class UploadImage
  {
    [Required(ErrorMessage = "Please select a file.")]
    [DataType(DataType.Upload)]
    [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".bmp" })]
    public IFormFile Image { get; set; }
  }
}
