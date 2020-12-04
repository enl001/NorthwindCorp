using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NorthwindCorp.Core.DTO;

namespace ConsoleHttpClient
{
  class Program
  {
    static HttpClient client = new HttpClient();

    static async Task<IEnumerable<ProductDto>> GetProductsAsync(string path)
    {
      List<ProductDto> products = new List<ProductDto>() { };
      HttpResponseMessage response = await client.GetAsync(path);
      if (response.IsSuccessStatusCode)
      {
        products = (await response.Content.ReadAsAsync<IEnumerable<ProductDto>>()).ToList();
      }
      return products;
    }

    static async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(string path)
    {
      List<CategoryDto> categories = new List<CategoryDto>() { };
      HttpResponseMessage response = await client.GetAsync(path);
      if (response.IsSuccessStatusCode)
      {
        categories = (await response.Content.ReadAsAsync<IEnumerable<CategoryDto>>()).ToList();
      }
      return categories;
    }

    static void ShowProducts(IEnumerable<ProductDto> products)
    {
      Console.WriteLine("PRODUCTS");
      var counter = 1;
      foreach (var product in products)
      {
        Console.WriteLine($"\t№:{counter++} Name: {product.ProductName} In stock: {product.UnitsInStock ?? 0}");
      }

      Console.WriteLine("-----------------------------");
    }
    static void ShowCategories(IEnumerable<CategoryDto> categories)
    {
      Console.WriteLine("CATEGORIES");
      var counter = 1;
      foreach (var category in categories)
      {
        Console.WriteLine($"\t№:{counter++} Name: {category.CategoryName} Description: {category.Description ?? "No description"}");
      }
      Console.WriteLine("-----------------------------");
    }

    static void Main(string[] args)
    {
      RunAsync().GetAwaiter().GetResult();
    }

    static async Task RunAsync()
    {
      client.BaseAddress = new Uri("https://localhost:44385/");
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

      try
      {
        var products = await GetProductsAsync("api/products");
        var categories = await GetCategoriesAsync("api/categories");
        ShowCategories(categories);
        ShowProducts(products);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

      Console.ReadLine();

    }
  }
}
