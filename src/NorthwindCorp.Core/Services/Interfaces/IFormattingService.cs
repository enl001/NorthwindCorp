namespace NorthwindCorp.Core.Services.Interfaces
{
  public interface IFormattingService
  {
    string ToMoney(decimal? value);
  }
}
