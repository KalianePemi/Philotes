using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Philotes.Models;
using Philotes.Models.Enums;
using Philotes.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
       private readonly DataContext _context;
       public PetController(DataContext context)
       {
           _context = context;
       }

        private static List<Pet>pets = new List<Pet>
        {
            new Pet(),
            new Pet {Id = 1, Nome = "Belota"},
            new Pet {Id = 2, Nome = "Cherry", Raca = "Lhasa e Shih Tzy", Cor = CorEnum.Cinza,  Descricao = "Invocada com lacinho", Porte=PorteEnum.P, Sexo=SexoEnum.Feminino},
            new Pet {Id = 4, Nome = "Cacau", Raca = "Labrador", Cor = CorEnum.Cinza, Descricao = "Toda gordinha é legal", Porte=PorteEnum.G, Sexo=SexoEnum.Feminino }
        };
        public Pet petObjeto = new Pet();

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try {
                return Ok(await _context.Pets.ToListAsync());
            } catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
           try
           {
               Pet petObjeto = await _context.Pets.FirstOrDefaultAsync(petBusca => petBusca.Id == id);
               return Ok (petObjeto);
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }
        
        }

        [HttpGet("GetQuantidade")]
        public async Task<IActionResult> GetQuantidade()
        {
            try
            {
                int count = await _context.Pets.CountAsync();
                return Ok("Quantidade de Pets: " + count);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }
         [HttpGet("GetOrdem")]
         public async Task <IActionResult> GetOrdem()
         {
            try
            {
            List<Pet> listaOrdem = _context.Pets.OrderBy(petObjeto => petObjeto.Porte).ToList();
                return Ok(listaOrdem);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }

         }

         [HttpGet("GetRacaAproximado/{raca}")]
       public  async  Task <IActionResult> GetRacaAproximado(string raca)
        {
          try
           {    List<Pet> listaRaca = await _context.Pets.Where(petObjeto => petObjeto.Raca.Contains(raca)).ToListAsync();
                return Ok(listaRaca);
           }
           catch (Exception ex)
           {
               return BadRequest(ex.Message);
           }
        }

        [HttpPost]
        public async Task <IActionResult> AddPet(Pet novoPet)
        {
            try
            {
                await _context.Pets.AddAsync(novoPet);
                await _context.SaveChangesAsync(); 
                return Ok(await _context.Pets.ToListAsync());
            }
            catch  (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost("ListarPorCores")]
        public IActionResult ListarPorCores(List<CorEnum> cores)
        {
            List<Pet> petCor = new List<Pet>{};
            foreach (var pet in pets) {
                foreach (var cor in cores) {
                    if (pet.Cor != null /*/&& pet.Cores.Contains(cor)*/) {
                        petCor.Add(pet);
                        break;
                    }
                }
            }
            return Ok(petCor);
        }
        [HttpPost("EnviarNotificacao")]
        public async Task <IActionResult> EnviarEmail()
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("philotesapp@gmail.com", "philotes123"),
                EnableSsl = false,
            };
    
            smtpClient.Send("philotesapp@gmail.com", "kaliane.pemi@gmail.com", "Teste", "Olá eu sou um teste de notificação");

            return Ok();
        }


        [HttpPut]
        public async Task <IActionResult> UpdatePet(Pet petObjeto)
        {
            try
            {
                Pet petAlterado = await _context.Pets.FirstOrDefaultAsync(qlqcoisa => qlqcoisa.Id == petObjeto.Id);

                petAlterado.Nome = petObjeto.Nome;
                petAlterado.Raca = petObjeto.Raca;
                petAlterado.Cor = petObjeto.Cor;
                petAlterado.Descricao = petObjeto.Descricao;
                petAlterado.Porte = petObjeto.Porte;
                petAlterado.Sexo = petObjeto.Sexo;

                _context.Pets.Update(petAlterado);
                int petAlteradoFim = await _context.SaveChangesAsync();

                return Ok(petAlteradoFim);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task <IActionResult> Delete(int Id)
        {
            try
            {
                Pet petAserExcluido = await _context.Pets.FirstOrDefaultAsync (qlqPet => qlqPet.Id == Id);
                _context.Pets.Remove(petAserExcluido);
                int petExcluido = await _context.SaveChangesAsync();
                return Ok(petExcluido);
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message);
            }
        }
   }
}