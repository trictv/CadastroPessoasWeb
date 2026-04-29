using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadastroPessoas.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        [StringLength(150)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Número é obrigatório.")]
        [StringLength(20)]
        public string Numero { get; set; }

        [StringLength(100)]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória.")]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2)]
        public string Uf { get; set; }
    }
}