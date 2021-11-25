using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/formapagamento")]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly DataContext _context;
        public FormaPagamentoController(DataContext context)
        {
            _context = context;
        }

        //POST: api/formapagamento/create
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] FormaPagamento formaPagamento)
        {
            _context.FormaPagamentos.Add(formaPagamento);
            _context.SaveChanges();
            return Created("", formaPagamento);
        }

        //GET: api/formapagamento/list
        [HttpGet]
        [Route("list")]
        public IActionResult List() => Ok(_context.FormaPagamentos.ToList());

    }
}