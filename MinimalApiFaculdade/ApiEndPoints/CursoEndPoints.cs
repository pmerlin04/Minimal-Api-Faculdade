using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class CursoEndPoints
    {
        public static void MapCursoEndPoints(this WebApplication app)
        {
            //METODOS CURSO
            app.MapGet("/cursos", async (AppDbContext db) =>
                await db.Cursos.ToListAsync()).WithTags("Cursos").RequireAuthorization();


            app.MapGet("/cursos{id}", async (int id, AppDbContext db) =>
                    await db.Cursos.FindAsync(id) is Curso curso ?
                    Results.Ok(curso) :
                    Results.NotFound($"curso {id} não encontrado")).WithTags("Cursos").RequireAuthorization();


            app.MapPost("/cursos", async (Curso curso, AppDbContext db) =>
            {
                db.Cursos.Add(curso);
                await db.SaveChangesAsync();

                return Results.Created($"/cursos/{curso.CursoId}", curso);

            }).WithTags("Cursos").RequireAuthorization();


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
            }).WithTags("Cursos").RequireAuthorization();



            app.MapDelete("/cursos/Deletar{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Cursos.FindAsync(id);
                if (result is null)
                {
                    return Results.NotFound($"curso {id} não encontrado");
                }

                db.Cursos.Remove(result);
                await db.SaveChangesAsync();
                return Results.Ok(result);
            }).WithTags("Cursos").RequireAuthorization();

        }

    }
}
