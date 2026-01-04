using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GestioneOrdiniRistorante.Models
{
    public class Utente 
    {
        [Key]
        public int Id { get; set; }
        public String Mail { get; set; }
        public String Nome { get; set; }
        public String Cognome { get; set; }
        public String Password { get; set; }
        public int Ruolo { get; set; } //( 1+ = Cliente / 0 = Amministratore)


        public Utente(String Mail, String Nome, String Cognome, String Password, int Ruolo) { 
            this.Mail = Mail;
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Password = Password;
            this.Ruolo = Ruolo;
        }


    }   
}
