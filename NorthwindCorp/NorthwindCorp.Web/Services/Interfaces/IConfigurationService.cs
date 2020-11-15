namespace NorthwindCorp.Web.Services.Interfaces
{
  public interface IConfigurationService
  {
    T GetValue<T>(string param);
  }
}
