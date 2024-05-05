using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.Context
{
    //TRANSFORMANDO AS ENTIDADES EM TABELAS NO BANCO DE DADOS
    class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //vai setar as entidades para colocar no banco
        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<Curso_Disciplina> Curso_Disciplinas => Set<Curso_Disciplina>();
        public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
        public DbSet<Departamento> Departamentos => Set<Departamento>();


    }


}
