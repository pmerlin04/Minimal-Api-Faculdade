using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class CursoDisciplinaEndPoints
    {
        public static void MapCursoDisciplinaEndPoints(this WebApplication app)
        {
            app.MapGet("/curso_disciplinas", async (AppDbContext db) =>
                await db.Curso_Disciplinas.ToListAsync()).WithTags("Curso Disciplinas").RequireAuthorization();


            app.MapGet("/curso_disciplinas{id}", async (int id, AppDbContext db) =>

                await db.Curso_Disciplinas.FindAsync(id) is Curso_Disciplina curso_Disciplina ?
                Results.Ok(curso_Disciplina) : Results.NotFound()).WithTags("Curso Disciplinas").RequireAuthorization();


            app.MapPost("/curso_disciplinas", async (Curso_Disciplina curso_disciplina, AppDbContext db) =>
            {
                db.Curso_Disciplinas.Add(curso_disciplina);
                await db.SaveChangesAsync();

                return Results.StatusCode(200);
            }).WithTags("Curso Disciplinas").RequireAuthorization();


            app.MapPut("/curso_disciplinas{id}", async (int id, Curso_Disciplina curso_disciplina, AppDbContext db) =>
            {
                var result = await db.Curso_Disciplinas.FindAsync(id);
                if (result is null)
                    return Results.NotFound();

                result.CursoId = curso_disciplina.CursoId;
                result.DisciplinaId = curso_disciplina.DisciplinaId;

                await db.SaveChangesAsync();
                return Results.Ok(result);

            }).WithTags("Curso Disciplinas").RequireAuthorization();

            app.MapDelete("/curso_disciplinas{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Curso_Disciplinas.FindAsync(id);
                if (result is null)
                    return Results.NotFound();

                db.Curso_Disciplinas.Remove(result);
                await db.SaveChangesAsync();

                return Results.Ok(result);

            }).WithTags("Curso Disciplinas").RequireAuthorization();
        }
    }
}
