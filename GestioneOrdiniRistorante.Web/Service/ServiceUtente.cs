using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;

namespace GestioneOrdiniRistorante.Service
{
    public class ServiceUtente : ServiceUtenteInt
    {

        private readonly UtenteRepo UtenteDB;
        public ServiceUtente(UtenteRepo UR)
        {
            UtenteDB = UR;
        }
        public async Task<Utente> CreaUtente(Utente T)
        {
            if (await UtenteDB.MailPresente(T.Mail))
            {
                UtenteDB.Add(T);
                await UtenteDB.SalvaDatiAsync();
                Console.WriteLine(T.Nome);
                return T;
            }
            else
                throw new Exception("mail Gia Presente");
            
        }



    }
}
