namespace CadastroPessoas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cep = c.String(nullable: false, maxLength: 9),
                        Logradouro = c.String(nullable: false, maxLength: 150),
                        Numero = c.String(nullable: false, maxLength: 20),
                        Complemento = c.String(maxLength: 100),
                        Bairro = c.String(nullable: false, maxLength: 100),
                        Cidade = c.String(nullable: false, maxLength: 100),
                        Uf = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        DataNascimento = c.DateTime(nullable: false),
                        EnderecoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.EnderecoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "EnderecoId", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "EnderecoId" });
            DropIndex("dbo.Pessoa", new[] { "Email" });
            DropTable("dbo.Pessoa");
            DropTable("dbo.Endereco");
        }
    }
}
