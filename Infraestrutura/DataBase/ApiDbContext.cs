using Dominio.Entidade;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.DataBase
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        { }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}