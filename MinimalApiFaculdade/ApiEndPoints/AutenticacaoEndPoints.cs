using Microsoft.AspNetCore.Authorization;
using MinimalApiFaculdade.Models;
using MinimalApiFaculdade.Services;

namespace MinimalApiFaculdade.ApiEndPoints
{
    public static class AutenticacaoEndPoints
    {
        public static void MapAutenticacaoEndPoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel user, ITokenService tokenservice) =>
            {
                if (user == null)
                    return Results.BadRequest("Login inválido");

                if (user.UserName == "Pedro Merlin" && user.Password == "123")
                {
                    var tokenString = tokenservice.GerarToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        user);
                    return Results.Ok(new
                    {
                        token = tokenString
                    });
                }
                else
                {
                    return Results.BadRequest("Login Invalidado");

                }

            }).Produces(StatusCodes.Status400BadRequest)
                               .Produces(StatusCodes.Status200OK)
                               .WithName("Login")
                               .WithTags("Autenticação");

        }
    }
}
