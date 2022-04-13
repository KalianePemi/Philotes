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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Evento eRemover = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
                _context.Eventos.Remove(eRemover);
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
                return Ok(await _context.Eventos.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut]
        public async Task<IActionResult> Update (Evento alteraEvento )
        {
            try
            {
                _context.Eventos.Update(alteraEvento);
                int linhaAfetada = await _context.SaveChangesAsync();
                return Ok(linhaAfetada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

 
