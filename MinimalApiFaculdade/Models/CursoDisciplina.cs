using System.Text.Json.Serialization;

namespace MinimalApiFaculdade.Models
{
    public class Curso_Disciplina
    {
        public int? Curso_disciplinaId
        {
            get; set;
        }
        public int? CursoId
        {
            get; set;
        }
        public int? DisciplinaId
        {
            get; set;
        }

        [JsonIgnore]
        public Curso? Curso
        {
            get; set;
        }

        [JsonIgnore]
        public Disciplina? Disciplina
        {
            get; set;
        }
    }

}
