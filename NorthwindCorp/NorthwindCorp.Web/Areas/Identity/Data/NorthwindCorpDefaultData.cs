using Microsoft.AspNetCore.Identity;

namespace NorthwindCorp.Web.Areas.Identity.Data
{
  public static class NorthwindCorpDefaultData
  {
    public static void Seed(UserManager<NorthwindCorpWebUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      SeedRoles(roleManager);
      SeedUsers(userManager);
    }

    private static void SeedUsers(UserManager<NorthwindCorpWebUser> userManager)
    {
      if (userManager.FindByEmailAsync("admin@northwind.com").Result == null)
      {
        NorthwindCorpWebUser user = new NorthwindCorpWebUser
        {
          UserName = "admin@northwind.com",
          NormalizedUserName = "ADMIN@NORTHWIND.COM",
          Email = "admin@northwind.com",
          NormalizedEmail = "ADMIN@NORTHWIND.COM",
          EmailConfirmed = true,
          LockoutEnabled = false,
        };

        IdentityResult result = userManager.CreateAsync(user, "asDF!234").Result; 
        if (result.Succeeded)
        {
          userManager.AddToRoleAsync(user, "Administrator").Wait();
        }
      }
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
      if (!roleManager.RoleExistsAsync("User").Result)
      {
        IdentityRole role = new IdentityRole();
        role.Name = "User";
        role.NormalizedName = "USER";
        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
      }

      if (!roleManager.RoleExistsAsync("Administrator").Result)
      {
        IdentityRole role = new IdentityRole();
        role.Name = "Administrator";
        role.NormalizedName = "ADMINISTRATOR";
        IdentityResult roleResult = roleManager.CreateAsync(role).Result;
      }
    }
  }
}
