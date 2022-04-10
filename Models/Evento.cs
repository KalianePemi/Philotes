using System.Collections.Generic;

namespace Philotes.Models

{
    public class Evento
    {
        public int Id {get; set;}
         public System.DateTime Data {get;set;}
         public Localizacao Localizacao {get; set;}
         public float Recompensa {get; set;}
         public Pet Pet { get; set; }
    }
}