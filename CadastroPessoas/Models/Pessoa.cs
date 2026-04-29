using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadastroPessoas.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(150)]
        [Index(IsUnique = true)] 
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public int? EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        public bool Tem18Anos()
        {
            var idade = GetIdade();
            if (DataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;
            return idade >= 18;
        }

        public int GetIdade()
        {
            return DateTime.Today.Year - DataNascimento.Year;
        }
    }
}