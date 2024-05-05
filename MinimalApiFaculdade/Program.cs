using MinimalApiFaculdade.ApiEndPoints;
using MinimalApiFaculdade.AppServicesExtensions;
using MinimalApiFaculdade.Context;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSwaggerGen();
//builder.AddApiSwagger();
builder.AddApiSwagger();
builder.Services.AddCors();
builder.AddAutenticationJwt();
builder.AddPersistenc();



var app = builder.Build();

//Minimal API de uma faculdade, com as entidade:
//ALUNO, CURSO, CURSO_DISCIPLINA, DISCIPLINA, DEPARTAMENTO

//ENDPOINT AUTENTICAÇÃO
app.MapAutenticacaoEndPoints();

//ENDPOINTS ALUNO
app.MapAlunoEndPoints();

//ENDPOINTS CURSO
app.MapCursoEndPoints();

//ENDPOINTS DISCIPLINA 
app.MapDisciplinaEndPoints();

//ENDPOINTS CURSO_DISCIPLINA
app.MapCursoDisciplinaEndPoints();

//ENDPOINTS DEPARTAMENTO
app.MapDepartamentoEndPoints();


var environment = app.Environment;

app.UseExceptionHandling(environment)
    .UseSwaggetMiddleware()
    .UseAppCors();

//autentificação e autorização do token
app.UseAuthentication();
app.UseAuthorization();

app.Run();












