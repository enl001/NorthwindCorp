using System.Globalization;
using NorthwindCorp.Core.Services.Interfaces;

namespace NorthwindCorp.Core.Services
{
  public class FormattingService : IFormattingService
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
