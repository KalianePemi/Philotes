
using Microsoft.EntityFrameworkCore;
using Philotes.Models;
using Philotes.Models.Enums;
//using System.Colletions.
namespace Philotes.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
       {

       }
        public DbSet<Pet> Pets {get; set;}
        public DbSet<Evento> Eventos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<PetCor> PetCores {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.Entity<Pet>().HasData
            (
                //Inserir as linhas " new Pet() {Id = 2,..." da lista de Pets}
                new Pet {Id = 5, Nome = "Chico", Raca = "SRD", PetCores = {}, Descricao = "O Gato mais de boa", Porte=PorteEnum.P, Sexo=SexoEnum.Masculino }
            );
            modelBuilder.Entity<Cor>().HasData
            (
                new Cor{ Id = 1, Nome = "Branco" }                
            );

            modelBuilder.Entity<PetCor>().HasKey(pc => new {pc.PetId, pc.CorId});

            modelBuilder.Entity<PetCor>().HasData
            (
                new PetCor(){ PetId = 5, CorId = 1 }
            );
       }      
    }
}