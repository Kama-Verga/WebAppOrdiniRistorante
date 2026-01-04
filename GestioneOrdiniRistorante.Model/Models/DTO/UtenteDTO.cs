using GestioneOrdiniRistorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class UtenteDTO
    {
        public String Mail { get; set; }
        public String Nome { get; set; }
        public String Cognome { get; set; }
        public String Password { get; set; }
        public int Ruolo { get; set; } 

        public UtenteDTO(Utente T)
        {
            this.Mail = T.Mail;
            this.Nome = T.Nome;
            this.Cognome = T.Cognome;
            this.Password = T.Password;
            this.Ruolo = T.Ruolo;
        }

    }
}
