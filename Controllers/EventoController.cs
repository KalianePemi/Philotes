using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Philotes.Data;
using Philotes.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
       private readonly DataContext _context;
       public EventoController(DataContext context)
       {
           _context = context;
       }

       [HttpPost]
        public async Task <IActionResult>AddEvento (Evento novoEvento)
        {
            try
            {  

                await _context.Eventos.AddAsync(novoEvento);
                await _context.SaveChangesAsync();
                return Ok (novoEvento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     try
        //     {
        //         Evento eRemover = await _context.Evento.FirstOrDefaultAsync(e => e.Id == id);
        //         _context.Evento.Remove(eRemover);
        //         int linhaAfetadas = await _context.SaveChangesAsync();

        //         return Ok(linhasAfetadas);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpGet("GetAll")]
        public async Task <IActionResult>GetAll()
        {
            try
            {
                return Ok(await _context.Eventos.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}

 
