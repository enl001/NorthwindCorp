using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
