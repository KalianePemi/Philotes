using Philotes.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Philotes.Models
{
    public class Pet
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Raca {get; set;}
        public List<PetCor> PetCores {get; set;}
        public PorteEnum Porte {get; set;}
        public SexoEnum Sexo {get; set;}

        public string Descricao {get; set;}

        public string FotoPet {get; set;}
        public string UltimoLocalVisto {get; set;}
        
        public int UsuarioId {get; set;}
    }
}