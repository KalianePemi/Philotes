using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Philotes.Models;
using Philotes.Models.Enums;

namespace Philotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private static List<Pet>pets = new List<Pet>
        {
            new Pet(),
            new Pet {Id = 1, Nome = "Belota"},
            new Pet {Id = 2, Nome = "Cherry", Raca = "Lhasa e Shih Tzy", Cores = new List<CorEnum>{CorEnum.Branca, CorEnum.Preta},  Descricao = "Invocada com lacinho", Porte=PorteEnum.P, Sexo=SexoEnum.Femea},
            new Pet {Id = 4, Nome = "Cacau", Raca = "Labrador", Cores = new List<CorEnum>{CorEnum.Laranja}, Descricao = "Toda gordinha é legal", Porte=PorteEnum.G, Sexo=SexoEnum.Femea }
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
        }











    }
}