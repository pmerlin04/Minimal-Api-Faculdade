using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class DisciplinaEndPoints
    {
        public static void MapDisciplinaEndPoints(this WebApplication app)
        {
            app.MapGet("/disciplinas", async (AppDbContext db) =>
                await db.Disciplinas.ToListAsync()).WithTags("Disciplinas").RequireAuthorization();

            app.MapGet("/disciplinas{id}", async (int id, AppDbContext db) =>
                await db.Disciplinas.FindAsync(id) is Disciplina disciplina ?
                Results.Ok(disciplina) : Results.NotFound($"disciplina {id} não encontrada")).WithTags("Departamentos").RequireAuthorization();


            app.MapPost("/disciplinas", async (Disciplina disciplina, AppDbContext db) =>
            {
                db.Disciplinas.Add(disciplina);
                await db.SaveChangesAsync();

                return Results.Ok(disciplina);

            }).WithTags("Disciplinas").RequireAuthorization();


            app.MapPut("/disciplinas{id}", async (int id, Disciplina inputDisciplina, AppDbContext db) =>
            {
                var result = await db.Disciplinas.FindAsync(id);

                if (result is null)
                    return Results.NotFound($"disciplina {id} não encontrada");

                result.NomeDisciplina = inputDisciplina.NomeDisciplina;
                result.DepartamentoId = inputDisciplina.DepartamentoId;
                await db.SaveChangesAsync();
                return Results.Ok(result);
            }).WithTags("Disciplinas").RequireAuthorization();


            app.MapDelete("/disciplinas{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Disciplinas.FindAsync(id);
                if (result is null)
                    return Results.NotFound($"disciplina {id} não encontrada");

                db.Disciplinas.Remove(result);
                await db.SaveChangesAsync();

                return Results.Ok(result);
            }).WithTags("Disciplinas").RequireAuthorization();
        }
    }
}
