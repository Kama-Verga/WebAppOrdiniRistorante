using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Models;
using Microsoft.EntityFrameworkCore;


namespace GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions
{
    public class TokenJWTRepo 
    {
        private MyDBContent DB;

        public TokenJWTRepo(MyDBContent T)
        {
            DB = T;
        }

        public Utente? GetUtente(string email, string password)
        {
            var utente = DB.Utente.Where(u => u.Mail == email && u.Password == password).FirstOrDefault();
            
            return utente;
        }


    }
}
