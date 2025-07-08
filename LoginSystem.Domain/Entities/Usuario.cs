using LoginSystem.Domain.Entities.Base;

namespace LoginSystem.Domain.Entities;

public class Usuario : EntityBase
{
    public Usuario(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
}