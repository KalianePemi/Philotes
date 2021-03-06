using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Philotes.Models;
using Philotes.Models.Enums;
using Philotes.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using AspCore_Email.Services;

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

        public Pet petObjeto = new Pet();

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try {
                return Ok(await _context.Pets.Include(us => us.PetCores)
                                             .ThenInclude(us => us.Cor).ToListAsync());
            } catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
           try
           {
               Pet petObjeto = await _context.Pets
                                             .Include(us => us.PetCores).ThenInclude(us => us.Cor)
               
               .FirstOrDefaultAsync(petBusca => petBusca.Id == id);
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
            List<Pet> listaOrdem = await _context.Pets.OrderBy(petObjeto => petObjeto.Porte).ToListAsync();
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

        // [HttpPost("ListarPorCores")]
        // public async Task<IActionResult> ListarPorCores(PetCor Cor)
        // {
        //     List<Pet> pets = new List<Pet>{};
        //     List<PetCor> petCores = await _context.PetCores.Where(PetCor => PetCor.CorId == Cor.CorId).ToListAsync();
        //     foreach (var petCor in petCores) {
        //         pets.Add(petCor.Pet);
        //     }
        //     return Ok(pets);
        // }

        [HttpPost("EnviarNotificacao/{Id}")]
        public async Task <IActionResult> EnviarNotificacao(int Id)
        {
            try {
                Pet pet = await _context.Pets.FirstOrDefaultAsync (qlqPet => qlqPet.Id == Id);
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Id == pet.UsuarioId);

                var emailSettings = new EmailSettings();
                emailSettings.PrimaryDomain = "smtp.gmail.com";
                emailSettings.PrimaryPort = 587;
                emailSettings.CcEmail = "kaliane.pemi@gmail.com";
                emailSettings.ToEmail = usuario.Email;
                emailSettings.FromEmail = "appphilotes@gmail.com";

                emailSettings.UsernamePassword = "ufrdspafdzgsyqvm"; 
                emailSettings.UsernameEmail = "appphilotes@gmail.com";

                emailSettings.Message = $"Ol?? tenho boas noticias {usuario.Nome}";
                emailSettings.Subject = $"Encontrei o pet {pet.Nome}";

                var emailService = new AuthMessageSender();
                await emailService.SendEmailAsync(emailSettings);
                return Ok();

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }

            // var smtpClient = new SmtpClient("smtp.gmail.com")
            // {
            //     Port = 587,
            //     Credentials = new NetworkCredential("philotesapp@gmail.com", "philotes123"),
            //     EnableSsl = false,
            // };
    
            // smtpClient.Send("philotesapp@gmail.com", "kaliane.pemi@gmail.com", "Teste", "Ol?? eu sou um teste de notifica????o");

        }


        [HttpPut]
        public async Task <IActionResult> UpdatePet(Pet petObjeto)
        {
            try
            {
                Pet petAlterado = await _context.Pets.FirstOrDefaultAsync(qlqcoisa => qlqcoisa.Id == petObjeto.Id);

                petAlterado.Nome = petObjeto.Nome;
                petAlterado.Raca = petObjeto.Raca;
                petAlterado.PetCores = petObjeto.PetCores;
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

        [HttpPost("PhotoUpload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadPetPhoto()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var petPhoto = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { petPhoto });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
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