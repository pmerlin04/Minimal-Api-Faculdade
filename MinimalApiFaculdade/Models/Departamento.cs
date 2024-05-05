using System.Text.Json.Serialization;

namespace MinimalApiFaculdade.Models
{

    public class Departamento
    {
        public int? DepartamentoId
        {
            get; set;
        }
        public string? NomeDepartamento
        {
            get; set;
        }
        public int? Andar
        {
            get; set;
        }
        public string? Bloco
        {
            get; set;
        }

        [JsonIgnore]
        public ICollection<Disciplina>? Disciplina
        {
            get; set;
        }
    }

}
