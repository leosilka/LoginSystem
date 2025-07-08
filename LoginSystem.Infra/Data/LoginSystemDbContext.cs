using LoginSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem.Infra.Data;

public class LoginSystemDbContext : DbContext
{
    public LoginSystemDbContext(DbContextOptions<LoginSystemDbContext> options) : base(options)
    {

    }

    public DbSet<Usuario> Usuarios { get; set; }
}