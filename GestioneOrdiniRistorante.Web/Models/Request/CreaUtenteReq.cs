using GestioneOrdiniRistorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Application.Models.Request
{
    public class CreaUtenteReq
    {
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Ruolo {  get; set; } = 0;


        public Utente ToEntity()
        {

            Utente utente = new Utente(Email, Nome, Cognome, Password, Ruolo);

            return utente;
        }
    }
}
