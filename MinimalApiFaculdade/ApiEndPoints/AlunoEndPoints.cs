using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class AlunoEndPoints
    {
        public static void MapAlunoEndPoints(this WebApplication app)
        {
            //METODOS ALUNO
            app.MapGet("/alunos", async (AppDbContext db) =>
                await db.Alunos.ToListAsync()).WithTags("Alunos").RequireAuthorization();

            app.MapGet("/alunos/{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Alunos.FindAsync(id);

                if (result is null)
                {
                    return Results.NotFound($"Não foi encontrado o Aluno{id}");
                }

                return Results.Ok(result);
            }).WithTags("Alunos").RequireAuthorization();

            app.MapPost("/alunos/Cadastrar", async (Aluno aluno, AppDbContext db) =>
            {
                db.Alunos.Add(aluno);
                await db.SaveChangesAsync();

                return Results.Ok(aluno);

            }).WithTags("Alunos").RequireAuthorization();

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
            }).WithTags("Alunos").RequireAuthorization();

            app.MapDelete("/alunos/Deletar{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Alunos.FindAsync(id);

                if (result is null)
                {
                    return Results.NotFound($"Não foi encontrado o Aluno{id}");
                }

                db.Alunos.Remove(result);
                await db.SaveChangesAsync();


                return Results.Ok(result);

            }).WithTags("Alunos").RequireAuthorization();
        }

    }
}
