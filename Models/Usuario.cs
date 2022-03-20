using System;
using Philotes.Models.Enums;

namespace Philotes.Models

{
    public class Usuario
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Sobrenome {get; set;}
        public string Email {get;set;}
        public int Celular {get; set;}
        public SexoEnum Sexo {get; set;}
        public DateTime Nascimento{get;set;}


    }
}



