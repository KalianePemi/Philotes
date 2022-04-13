using Philotes.Models.Enums;
using System.Collections.Generic;

namespace Philotes.Models
{
    public class Pet
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Raca {get; set;}
        public CorEnum Cor {get; set;}
        public PorteEnum Porte {get; set;}
        public SexoEnum Sexo {get; set;}

        public string Descricao {get; set;}

        public byte[] FotoPet {get; set;}
        public Usuario Usuario {get; set;}
    }
}