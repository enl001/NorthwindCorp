namespace NorthwindCorp.Web.Services.Interfaces
{
  public interface IFormattingService
  {
    string ToMoney(decimal? value);

    string ToYesNo(bool value);
  }
}
