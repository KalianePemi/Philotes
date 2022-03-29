using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Philotes.Models;
using Philotes.Models.Enums;
using Philotes.Data;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
       /*private readonly DataContext _context;
       public PersonagemController(DataContext context)
       {
           _context = context;
       }
       [HttpGet("{id}")]
       public async Task<IActionResult> GetSingle(int id)
       {
           Try
           {
               Pet petObjeto = await _context.Pets.FirstOrDefaultAsync(petBusca =>petBusca.Id == id);
               return Ok(petObjeto);

           }
           catch (Exception ex)
           {
               return BadRequest (ex.Message);
           }*/

       
        private static List<Pet>pets = new List<Pet>
        {
            new Pet(),
            new Pet {Id = 1, Nome = "Belota"},
            new Pet {Id = 2, Nome = "Cherry", Raca = "Lhasa e Shih Tzy", Cor = CorEnum.Cinza,  Descricao = "Invocada com lacinho", Porte=PorteEnum.P, Sexo=SexoEnum.Feminino},
            new Pet {Id = 4, Nome = "Cacau", Raca = "Labrador", Cor = CorEnum.Cinza, Descricao = "Toda gordinha é legal", Porte=PorteEnum.G, Sexo=SexoEnum.Feminino }
        };
        private Pet petObjeto = new Pet();

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(pets.FirstOrDefault(petObjeto => petObjeto.Id ==id));
        }

        [HttpGet("GetQuantidade")]
        public IActionResult GetQuantidade()
        {
            return Ok ("Quantidade de Pets: " + pets.Count);
        }

        [HttpGet("GetOrdem")]
        public IActionResult GetOrdem()
        {
            List<Pet> listaOrdem = pets.OrderBy(petObjeto => petObjeto.Porte).ToList();
            return Ok(listaOrdem);
        }

        [HttpGet("GetRacaAproximado/{raca}")]
        public IActionResult GetRacaAproximado(string raca)
        {
            List<Pet> listaRaca = pets.FindAll(petObjeto => petObjeto.Raca.Contains(raca));
            return Ok(listaRaca);
        }

        [HttpPost]
        public IActionResult AddPet(Pet novoPet)
        {
            pets.Add(novoPet);
            return Ok(pets);
        }// branco, preto
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
        public IActionResult EnviarEmail()
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
        public IActionResult UpdatePet(Pet petObjeto)
        {
            Pet petAlterado = pets.Find(qlqcoisa => qlqcoisa.Id == petObjeto.Id);
            petAlterado.Nome = petObjeto.Nome;
            petAlterado.Raca = petObjeto.Raca;
            petAlterado.Cor = petObjeto.Cor;
            petAlterado.Descricao = petObjeto.Descricao;
            petAlterado.Porte = petObjeto.Porte;
            petAlterado.Sexo = petObjeto.Sexo;
            return Ok(petAlterado);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            pets.RemoveAll(qlqPet => qlqPet.Id == Id);
            return Ok(pets);
        }


   }
}