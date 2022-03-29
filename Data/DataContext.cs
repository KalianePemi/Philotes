
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
        
       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
        modelBuilder.Entity<Pet>().HasData
        (
            //Inserir as linhas " new Pet() {Id = 2,..." da lista de Pets}
             new Pet {Id = 5, Nome = "Chico", Raca = "SRD", Cor = CorEnum.Cinza, Descricao = "O Gato mais de boa", Porte=PorteEnum.P, Sexo=SexoEnum.Masculino }
        );
       //Área para futuros Inserts no banco
       }
        
    }
}