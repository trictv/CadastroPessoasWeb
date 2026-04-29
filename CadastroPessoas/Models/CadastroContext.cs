using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CadastroPessoas.Models
{
    public class CadastroContext : DbContext
    {
        public CadastroContext() : base("CadastroPessoas")
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}