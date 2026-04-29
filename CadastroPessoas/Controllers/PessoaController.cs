using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CadastroPessoas.Models;

namespace CadastroPessoas.Controllers
{
    public class PessoaController : Controller
    {
        private CadastroContext db = new CadastroContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Listar()
        {
            var pessoas = db.Pessoas.Include(p => p.Endereco).Select(p => new {
                p.Id,
                p.Nome,
                p.Email,
                DataNascimento = p.DataNascimento,
                Endereco = p.Endereco != null ? new
                {
                    p.Endereco.Cep,
                    p.Endereco.Logradouro,
                    p.Endereco.Bairro,
                    p.Endereco.Cidade,
                    p.Endereco.Uf
                } : null
            }).ToList();

            return Json(pessoas, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Salvar(Pessoa pessoa)
        {
            try
            {
                if (!pessoa.Tem18Anos())
                    return Json(new { sucesso = false, mensagem = $"O registo só pode ser feito por maiores de 18 anos. você tem apenas {pessoa.GetIdade()} anos" });

                // Regra 2: Validação de E-mail Único
                var emailExiste = db.Pessoas.Any(p => p.Email == pessoa.Email && p.Id != pessoa.Id);
                if (emailExiste)
                    return Json(new { sucesso = false, mensagem = "Este e-mail já está em uso." });

                if (pessoa.Id == 0)
                {
                    db.Pessoas.Add(pessoa);
                }
                else
                {
                    db.Entry(pessoa).State = EntityState.Modified;
                    if (pessoa.Endereco != null)
                    {
                        if (pessoa.Endereco.Id == 0)
                            db.Enderecos.Add(pessoa.Endereco);
                        else
                            db.Entry(pessoa.Endereco).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
                return Json(new { sucesso = true, mensagem = "Salvo com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao salvar: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var pessoa = db.Pessoas.Find(id);
            if (pessoa != null)
            {
                if (pessoa.EnderecoId.HasValue)
                {
                    var endereco = db.Enderecos.Find(pessoa.EnderecoId);
                    if (endereco != null) db.Enderecos.Remove(endereco);
                }

                db.Pessoas.Remove(pessoa);
                db.SaveChanges();
                return Json(new { sucesso = true, mensagem = "Excluído com sucesso!" });
            }
            return Json(new { sucesso = false, mensagem = "Pessoa não encontrada." });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}