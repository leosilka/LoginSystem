namespace LoginSystem.Domain.Entities.Base;

public abstract class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CriadoEm { get; protected set; } = DateTime.Now;
    public DateTime? EditadoEm { get; protected set; }
    public bool Ativo { get; protected set; } = true;
    public string CriadoPor { get; protected set; } = string.Empty;
    public string EditadoPor { get; protected set; } = string.Empty;
    public string CriadoIp { get; protected set; } = string.Empty;
    public string EditadoIp { get; protected set; } = string.Empty;

    protected EntityBase()
    {
        CriadoEm = DateTime.Now;
        Ativo = true;
    }

    /// <summary>
    /// Marca a entidade como criada, preenchendo usuário e IP de origem.
    /// </summary>
    /// <param name="usuario">Nome ou identificador do usuário responsável pela criação.</param>
    /// <param name="ip">Endereço IP de onde a criação foi realizada.</param>
    public void MarcarComoCriado(string usuario, string ip)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            throw new ArgumentException("Usuário não informado.", nameof(usuario));

        if (string.IsNullOrWhiteSpace(ip))
            throw new ArgumentException("IP não informado.", nameof(ip));

        CriadoPor = usuario;
        CriadoIp = ip;
    }

    /// <summary>
    /// Marca a entidade como editada, preenchendo usuário e IP de edição.
    /// </summary>
    /// <param name="usuario">Nome ou identificador do usuário responsável pela edição.</param>
    /// <param name="ip">Endereço IP de onde a edição foi realizada.</param>
    public void MarcarComoEditado(string usuario, string ip)
    {
        if(string.IsNullOrWhiteSpace(usuario))
            throw new ArgumentException("Usuário não informado.", nameof(usuario));

        if(string.IsNullOrWhiteSpace(ip))
            throw new ArgumentException("IP não informado.", nameof(ip));

        EditadoEm = DateTime.Now;
        EditadoPor = usuario;
        EditadoIp = ip;
    }

    /// <summary>
    /// Marca a entidade como inativa.
    /// </summary>
    public void Desativar()
    {
        Ativo = false;
    }
}