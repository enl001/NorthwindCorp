using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindCorp.Web.Data;
using System.Threading.Tasks;

namespace NorthwindCorp.Web.Controllers
{
  [Authorize(Roles = "Administrator")]
  public class UsersController : Controller
  {
    private readonly NorthwindCorpWebIdentityContext _identityDbContext;

    public UsersController(NorthwindCorpWebIdentityContext identityDbContext)
    {
      _identityDbContext = identityDbContext;
    }

    public async Task<IActionResult> Index()
    {
      var users = await _identityDbContext.Users.ToListAsync();
      return View(users);
    }
  }
}
