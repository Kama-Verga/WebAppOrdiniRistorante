namespace GestioneOrdiniRistorante.Web.Extension
{
    public static class MiddlewareExtension
    {
        public static WebApplication? AddWebMiddleware(this WebApplication? app)
        {
            if (app == null) return null;

            if (app.Environment.IsDevelopment())    // Attiva Swagger solo in ambiente di sviluppo per la documentazione API
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();              // Impone l'uso di HTTPS reindirizzando le richieste HTTP a HTTPS
            app.UseAuthentication();                // Abilita l'autenticazione: verifica le credenziali presenti nelle richieste
            app.UseAuthorization();                 // Abilita l'autorizzazione: determina se un utente autenticato ha il permesso di accedere a risorse specifiche
            app.MapControllers();                   // Mappa i controller all'interno dell'applicazione per gestire le richieste

            return app;
        }
    }
}
