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



            return Created("", venda);
        }

        //GET: api/venda/list
        //ALTERAR O MÉTODO PARA MOSTRAR TODOS OS DADOS DA VENDA E OS DADOS RELACIONADOS
        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            var vendas = _context.Vendas.Include(v => v.FormaPagamento)
                                        .Include(v => v.Itens)
                                        .ThenInclude(i => i.Produto)
                                        .ThenInclude(p => p.Categoria)
                                        .AsNoTracking()
                                        .ToList();

            return Ok(vendas);
        }
    }
}