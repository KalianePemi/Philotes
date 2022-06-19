using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Philotes.Data;
using Philotes.Models;


namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly DataContext _context;
        public LocalizacaoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <IActionResult>AddLocal (Localizacao novoLocal)
        {
            try
            {  
                await _context.Enderecos.AddAsync(novoLocal);
                await _context.SaveChangesAsync();
                return Ok (novoLocal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update (Localizacao alteraLocal )
        {
            try
            {
                _context.Enderecos.Update(alteraLocal);
                int linhaAfetada = await _context.SaveChangesAsync();
                return Ok(linhaAfetada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task <IActionResult>GetAll()
        {
            try
            {
                return Ok(await _context.Enderecos.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }    
    }  
}