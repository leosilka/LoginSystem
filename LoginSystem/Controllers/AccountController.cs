using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers;

public class AccountController : Controller
{
    // GET: /Account/Index
    [Authorize]
    public IActionResult Index() => View();

    // GET: /Account/Dashboard
    [Authorize]
    public IActionResult Dashboard() => View();
}
