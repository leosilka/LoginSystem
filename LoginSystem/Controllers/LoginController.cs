using BCrypt.Net;
using LoginSystem.Domain.Entities;
using LoginSystem.Domain.Interfaces.Repositories;
using LoginSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginSystem.Controllers;

public class LoginController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    // GET: Login/Index
    public IActionResult Index() => View();

    // GET: Login/Registro
    public IActionResult Registro() => View();

    // POST: Login/Registro
    [HttpPost]
    public async Task<IActionResult> Registro(RegistroViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Verifique se o e-mail já está cadastrado
        var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(model.Email);

        if (usuarioExistente != null)
        {
            ModelState.AddModelError(string.Empty, "E-mail já cadastrado.");
            return View(model);
        }

        // Gere o hash da senha
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(model.Senha);
        var usuario = new Usuario(model.Nome, model.Email, senhaHash);

        await _usuarioRepository.AdicionarAsync(usuario);

        TempData["RegistroSucesso"] = true;

        return RedirectToAction("Index", "Login");
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var usuario = await _usuarioRepository.ObterPorEmailAsync(model.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha))
        {
            TempData["ErroLogin"] = true;
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var claimsIdentify = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentify));

        // Redireciona para o Index de Account
        return RedirectToAction("Index", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}