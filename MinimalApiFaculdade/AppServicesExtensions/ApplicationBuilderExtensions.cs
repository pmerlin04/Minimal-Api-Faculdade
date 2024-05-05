namespace MinimalApiFaculdade.AppServicesExtensions
{
    public static class ApplicationBuilderExtensions
    {

        //verifica se o ambiente é de desenvolvedor
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            return app;
        }

        //habilitando o CORS
        //O CORS serve para dar permissões de acesso, metodos e cabeçalhos em uma aplicação
        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors(p =>
            {

                p.AllowAnyOrigin();//permissão de qualquer origem
                p.WithMethods("GET");//somente metodos GET
                p.AllowAnyHeader();//permissao de qualquer cabeçalho
            });
            return app;
        }

        public static IApplicationBuilder UseSwaggetMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { });
            return app;
        }
    }
}
