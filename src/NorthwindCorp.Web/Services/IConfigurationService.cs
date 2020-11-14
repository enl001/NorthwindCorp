namespace NorthwindCorp.Web.Services
{
  public interface IConfigurationService
  {
    T GetValue<T>(string param);
  }
}
