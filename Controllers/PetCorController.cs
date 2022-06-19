using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Philotes.Data;
using Philotes.Models;
using Philotes.Models.Enums;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetCorController : ControllerBase
    {
        private readonly DataContext _context;
        public PetCorController(DataContext context)
        {
            _context = context;
        }

         [HttpPost("ListarPorCores")]
        public async Task<IActionResult> ListarPorCores(PetCor Cor)
        {
            List<Pet> pets = new List<Pet>{};
            List<PetCor> petCores = await _context.PetCores.Where(PetCor => PetCor.CorId == Cor.CorId).ToListAsync();
            foreach (var petCor in petCores) {
                pets.Add(petCor.Pet);
            }
            return Ok(pets);
        }
        // PetCor pc = new PetCor();
        // pc.Pet = pet;
        // pc.Cor = cor;
        // await _context.PetCor.AddAsync(pc);
        // int linhasAfetadas = await _context.SaveChangesAsync();

       // return Ok(linhasAfetadas)
    }
}

