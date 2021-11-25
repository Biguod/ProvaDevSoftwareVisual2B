using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/venda")]
    public class VendaController : ControllerBase
    {
        private readonly DataContext _context;
        public VendaController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Venda venda)
        {
            _context.Add(venda);
            _context.SaveChanges();

            venda = _context.Vendas.Include(v => v.FormaPagamento)
                                   .Include(v => v.Itens).ThenInclude(i => i.Produto)
                                   .ThenInclude(p => p.Categoria)
                                   .First(f => f.VendaId == venda.VendaId);

            return Created("", venda);
        }

        //GET: api/venda/list
        //ALTERAR O MÉTODO PARA MOSTRAR TODOS OS DADOS DA VENDA E OS DADOS RELACIONADOS
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            var vendas = _context.Vendas.Include(v => v.FormaPagamento)
                       .Include(v => v.Itens).ThenInclude(i => i.Produto)
                       .ThenInclude(p => p.Categoria)
                       .ToList();

            return Ok(vendas);
        }
    }
}