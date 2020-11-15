using System.Globalization;
using NorthwindCorp.Web.Services.Interfaces;

namespace NorthwindCorp.Web.Services
{
  public class FormattingService
  {
    public string ToYesNo(bool value)
    {
      return (value)
        ? "Yes"
        : "No";
    }

    public string ToMoney(decimal? value)
    {
      return value?.ToString("C", new CultureInfo("en-US"));
    }
  }
}
