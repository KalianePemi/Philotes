using System.Text.Json.Serialization;

namespace Philotes.Models.Enums
{
    public class PetCor
    {
        public int PetId {get; set;}

        [JsonIgnore]
        public Pet Pet {get; set;}

        public int CorId {get; set;}
        public Cor Cor {get; set;}
    }
}