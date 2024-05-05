using Microsoft.EntityFrameworkCore;
using MinimalApiFaculdade.Context;
using MinimalApiFaculdade.Models;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class DepartamentoEndPoints
    {
        public static void MapDepartamentoEndPoints(this WebApplication app)
        {
            app.MapGet("/departamento", async (AppDbContext db) =>
            await db.Departamentos.ToListAsync()).WithTags("Departamentos").RequireAuthorization();

            app.MapGet("/departamento{id}", async (int id, AppDbContext db) =>
                await db.Departamentos.FindAsync(id) is Departamento departamento ?
                Results.Ok(departamento) : Results.NotFound($"departamento {id} não encontrado")).WithTags("Departamentos").RequireAuthorization();

            app.MapPost("/departamento", async (Departamento departamento, AppDbContext db) =>
            {
                db.Departamentos.Add(departamento);
                await db.SaveChangesAsync();
            }).WithTags("Departamentos").RequireAuthorization();

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

            }).WithTags("Departamentos").RequireAuthorization();

            app.MapDelete("/departamento{id}", async (int id, AppDbContext db) =>
            {
                var result = await db.Departamentos.FindAsync(id);
                if (result is null)
                    return Results.NotFound($"departamento {id} não encontrado");

                db.Departamentos.Remove(result);
                await db.SaveChangesAsync();

                return Results.Ok(result);
            }).WithTags("Departamentos").RequireAuthorization();
        }

    }
}
