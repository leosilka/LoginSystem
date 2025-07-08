using LoginSystem.Domain.Entities;
using LoginSystem.Domain.Interfaces.Repositories;
using LoginSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly LoginSystemDbContext _context;

    public UsuarioRepository(LoginSystemDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        //await _context.Set<Usuario>().AddAsync(usuario);
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Email == email);
    }
}