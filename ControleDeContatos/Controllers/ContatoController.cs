using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio) 
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
                
        public IActionResult Apagar(int id)
        {
            _contatoRepositorio.Apagar(id);
            return RedirectToAction("Index"); // voltar para a página index
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            if (ModelState.IsValid) // se a informação do ModelState é válida (validações dos campos da tela)
            {
                _contatoRepositorio.Adicionar(contato);  // adiciona o contato no banco de dados
                return RedirectToAction("Index"); // voltar para a página index
            }            
            return View(contato);  // se não for válida retorna para a view passando o objeto contato
        }
        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            _contatoRepositorio.Atualizar(contato);  // adiciona o contato no banco de dados
            return RedirectToAction("Index"); // voltar para a página index
        }
    }
}
