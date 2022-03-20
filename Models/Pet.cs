using Philotes.Models.Enums;
using System.Collections.Generic;

namespace Philotes.Models
{
    public class Pet
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Raca {get; set;}
        public List<CorEnum> Cores {get; set;}
        public PorteEnum Porte {get; set;}
        public SexoEnum Sexo {get; set;}

        public string Descricao {get; set;}

        public string FotoURL {get; set;}
    }
}