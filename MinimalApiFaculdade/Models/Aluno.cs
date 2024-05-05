using System.Text.Json.Serialization;

namespace MinimalApiFaculdade.Models
{
    public class Aluno
    {
        public int? AlunoId
        {
            get; set;
        }
        public string? NomeAluno
        {
            get; set;
        }
        public string? Cpf
        {
            get; set;
        }
        public int? CursoId
        {
            get; set;
        }

        [JsonIgnore]
        public Curso? Curso
        {
            get; set;
        }

    }
}
