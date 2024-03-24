using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SERVICO DO BANCO EM MEMORIA COM O APPDBCONTEXT
builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("FaculdadeDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Minimal API de uma faculdade, com as entidade:
//ALUNO, CURSO, DISCIPLINA, DEPARTAMENTO


//METODOS ALUNO
app.MapGet("/alunos", async (AppDbContext db) =>
    await db.Alunos.ToListAsync());

app.MapGet("/alunos/{id}", async (int id, AppDbContext db) =>
{
    var result = await db.Alunos.FindAsync(id);

    if (result is null)
    {
        return Results.NotFound($"Não foi encontrado o Aluno{id}");
    }

    return Results.Ok(result);
});

app.MapPost("/alunos/Cadastrar", async (Aluno aluno, AppDbContext db) =>
{
    db.Alunos.Add(aluno);
    await db.SaveChangesAsync();

    return Results.Ok(aluno);
    
});

app.MapPut("/alunos/Mudar", async (int id, Aluno alunoModificado, AppDbContext db) =>
{
    var result = await db.Alunos.FindAsync(id);
    if (result is null)
    {
        return Results.NotFound($"Não foi encontrado o Aluno{id}");
    }

    result.NomeAluno = alunoModificado.NomeAluno;
    result.Cpf = alunoModificado.Cpf;
    result.CursoId = alunoModificado.CursoId;
    await db.SaveChangesAsync();

    return Results.Ok(result);
});

app.MapDelete("/alunos/Deletar{id}", async (int id, AppDbContext db) =>
{
    var result = await db.Alunos.FindAsync(id);

    if(result is null)
    {
        return Results.NotFound($"Não foi encontrado o Aluno{id}");
    }

    db.Alunos.Remove(result);
    await db.SaveChangesAsync();


    return Results.Ok(result);

});

//METODOS CURSO
app.MapGet("/cursos", async (AppDbContext db) =>
    await db.Cursos.ToListAsync());


app.MapGet("/cursos{id}", async (int id, AppDbContext db) =>
        await db.Cursos.FindAsync(id) is Curso curso ? 
        Results.Ok(curso) :
        Results.NotFound($"curso {id} não encontrado"));


app.MapPost("/cursos", async (Curso curso, AppDbContext db) =>
{
    db.Cursos.Add(curso);
    await db.SaveChangesAsync();

    return Results.Created($"/cursos/{curso.CursoId}", curso);

});


app.MapPut("/cursos{id}", async (int id, Curso inputCurso, AppDbContext db) =>
{
    var result = await db.Cursos.FindAsync(id);
    if (result is null)
    {
        return Results.NotFound($"curso {id} não encontrado");
    }
    result.NomeCurso = inputCurso.NomeCurso;
    
    await db.SaveChangesAsync();

    return Results.Ok(result);
});



app.MapDelete("/cursos/Deletar{id}", async (int id, AppDbContext db) =>
{
    var result = await db.Cursos.FindAsync(id);
    if ( result is null )
    {
        return Results.NotFound($"curso {id} não encontrado");
    }

    db.Cursos.Remove(result);
    await db.SaveChangesAsync();
    return Results.Ok(result);
});



//METODOS DISCIPLINA 
app.MapGet("/disciplinas", async (AppDbContext db) =>
{
    return db.Disciplinas.ToListAsync();
});

app.MapGet("/disciplinas{id}", async (int id, AppDbContext db) =>
    await db.Disciplinas.FindAsync(id) is Disciplina disciplina ?
    Results.Ok(disciplina) : Results.NotFound($"disciplina {id} não encontrada"));


app.MapPost("/disciplinas", async (Disciplina disciplina, AppDbContext db) =>
{
    db.Disciplinas.Add(disciplina);
    await db.SaveChangesAsync();

    return Results.Ok(disciplina);

});


app.MapPut("/disciplinas{id}", async (int id, Disciplina inputDisciplina, AppDbContext db) =>
{
     var result = await db.Disciplinas.FindAsync(id);

     if (result is null)
         return Results.NotFound($"disciplina {id} não encontrada");

     result.NomeDisciplina = inputDisciplina.NomeDisciplina;
     result.DepartamentoId = inputDisciplina.DepartamentoId;
     await db.SaveChangesAsync();
     return Results.Ok(result);
});


app.MapDelete("/disciplinas{id}", async (int id, AppDbContext db) =>
{
    var result = await db.Disciplinas.FindAsync(id);
    if (result is null)
        return Results.NotFound($"disciplina {id} não encontrada");

    db.Disciplinas.Remove(result);
    await db.SaveChangesAsync();

    return Results.Ok(result);
});


//METODOS CURSO_DISCIPLINA
app.MapGet("/curso_disciplinas", async (AppDbContext db) =>
{
    return db.Curso_Disciplinas.ToListAsync();
});

app.MapGet("/curso_disciplinas{id}", async (int id, AppDbContext db) =>

    await db.Curso_Disciplinas.FindAsync(id) is Curso_Disciplina curso_Disciplina ?
    Results.Ok(curso_Disciplina) : Results.NotFound());


app.MapPost("/curso_disciplinas", async (Curso_Disciplina curso_disciplina, AppDbContext db) =>
{
    db.Curso_Disciplinas.Add(curso_disciplina);
    await db.SaveChangesAsync();

    return Results.StatusCode(200);
});


app.MapPut("/curso_disciplinas{id}", async (int id, Curso_Disciplina curso_disciplina, AppDbContext db) =>
{
    var result = await db.Curso_Disciplinas.FindAsync(id);
    if (result is null)
        return Results.NotFound();

    result.CursoId = curso_disciplina.CursoId;
    result.DisciplinaId = curso_disciplina.DisciplinaId;

    await db.SaveChangesAsync();
    return Results.Ok(result);

});

app.MapDelete("/curso_disciplinas{id}", async (int id, AppDbContext db) =>
{
   var result = await db.Curso_Disciplinas.FindAsync(id);
    if (result is null)
        return Results.NotFound();

    db.Curso_Disciplinas.Remove(result);
    await db.SaveChangesAsync();

    return Results.Ok(result);
    
});


//METODOS DEPARTAMENTO
app.MapGet("/departamento", async (AppDbContext db) =>
     await db.Departamentos.ToListAsync());

app.MapGet("/departamento{id}", async (int id, AppDbContext db) =>
    await db.Departamentos.FindAsync(id) is Departamento departamento ?
    Results.Ok(departamento) : Results.NotFound($"departamento {id} não encontrado"));

app.MapPost("/departamento", async (Departamento departamento, AppDbContext db) =>
{
    db.Departamentos.Add(departamento);
    await db.SaveChangesAsync();
});

app.MapPut("/departament{id}", async (int id, Departamento departamento, AppDbContext db) =>
{
    var result = await db.Departamentos.FindAsync(id);
    if (result is null)
        return Results.NotFound($"departamento {id} não encontrado");

    result.NomeDepartamento = departamento.NomeDepartamento;
    result.Andar = departamento.Andar;
    result.Bloco = departamento.Bloco;

    await db.SaveChangesAsync();
    return Results.Ok(result);
    
});

app.MapDelete("/departamento{id}", async (int id, AppDbContext db) =>
{
    var result = await db.Departamentos.FindAsync(id);
    if (result is null)
        return Results.NotFound($"departamento {id} não encontrado");

    db.Departamentos.Remove(result);
    await db.SaveChangesAsync();

    return Results.Ok(result);
});
    



app.Run();

//CRIANDO AS ENTIDADES (CLASSES)
class Aluno
{
    public int? AlunoId { get; set; }
    public string? NomeAluno { get; set; }
    public string? Cpf { get; set; }
    public int? CursoId { get; set; }

    [JsonIgnore]
    public Curso? Curso { get; set; }

}

class Curso
{
    public int? CursoId { get; set; }
    public string? NomeCurso { get; set; }

    [JsonIgnore]
    public ICollection<Aluno>? Aluno { get; set; }
}


class Curso_Disciplina
{
    public int? Curso_disciplinaId { get; set; }
    public int? CursoId { get; set; }
    public int? DisciplinaId { get; set; }
    public Curso? Curso { get; set; }
    public Disciplina? Disciplina { get; set; }
}

class Disciplina
{
    public int? DisciplinaId { get; set; }
    public string? NomeDisciplina { get; set; }
    public int? DepartamentoId { get; set; }

    [JsonIgnore]
    public Departamento? Departamento { get; set; }
}

class Departamento
{
    public int? DepartamentoId { get; set; }
    public string? NomeDepartamento { get; set; }
    public int? Andar { get; set; }
    public string? Bloco { get; set; }

    [JsonIgnore]
    public ICollection<Disciplina>? Disciplina { get; set; }
}

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


