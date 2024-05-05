using System.Text.Json.Serialization;

namespace MinimalApiFaculdade.Models
{
    public class Curso
    {
        public int? CursoId
        {
            get; set;
        }
        public string? NomeCurso
        {
            get; set;
        }

        [JsonIgnore]
        public ICollection<Aluno>? Aluno
        {
            get; set;
        }
    }
}
