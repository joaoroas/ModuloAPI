using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Entities;
using ModuloAPI.Persistence;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly Context _context;
        public ContatoController(Context context)
        {
            _context = context;
        }

        [HttpPost("CriarContato")]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new {id = contato.Id}, contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var contato = _context.Contatos.Find(id);

            if(contato == null)
            {
                return NotFound();
            }

            return Ok(contato);
            
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var contatoDb = _context.Contatos.Find(id);

            if(contatoDb == null)
            {
                return NotFound();
            }

            contatoDb.Nome = contato.Nome;
            contatoDb.Telefone = contato.Telefone;
            contatoDb.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoDb);
            _context.SaveChanges();

            return Ok(contatoDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contatoDb = _context.Contatos.Find(id);

            if(contatoDb == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(contatoDb);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("BuscarPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));

            return Ok(contatos);
        }

        [HttpGet("BuscarTodos")]
        public List<Contato> BuscarTodosContatos()
        {
            return _context.Contatos.ToList();
        }
    }
}