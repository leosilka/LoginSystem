using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers;

public class AccountController : Controller
{
    // GET: /Account/Dashboard
    [Authorize]
    public IActionResult Dashboard() => View();
}
