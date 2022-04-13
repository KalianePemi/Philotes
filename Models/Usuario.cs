using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Philotes.Models.Enums;

namespace Philotes.Models

{
    public class Usuario
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Sobrenome {get; set;}
        public string Username {get; set;}
        public string Email {get;set;}
        public int Celular {get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        public byte[] Foto {get; set;}
        public SexoEnum Sexo {get; set;}
        public DateTime Nascimento{get;set;}
        public DateTime? DataAcesso {get; set;}

        [NotMapped]
        public string PasswordString {get; set;}
        public List<Pet> Pets {get; set;}


    }
}



