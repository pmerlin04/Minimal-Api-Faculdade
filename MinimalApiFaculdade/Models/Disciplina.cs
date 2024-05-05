using System.Text.Json.Serialization;

namespace MinimalApiFaculdade.Models
{
    public class Disciplina
    {
        public int? DisciplinaId
        {
            get; set;
        }
        public string? NomeDisciplina
        {
            get; set;
        }
        public int? DepartamentoId
        {
            get; set;
        }

        [JsonIgnore]
        public Departamento? Departamento
        {
            get; set;
        }
    }
}
