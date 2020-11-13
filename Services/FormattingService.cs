using System.Globalization;

namespace NorthwindCorp.Services
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
