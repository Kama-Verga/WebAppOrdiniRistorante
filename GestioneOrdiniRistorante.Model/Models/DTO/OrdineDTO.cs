using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class OrdineDTO
    {
        public int UtenteId { get; set; }
        public DateTime Data_creazione { get; set; }
        public int Numero_Ordine { get; set; }
        public String Indirizzo_Di_Consegna { get; set; }
        public decimal Prezzo { get; set; }

        public List<Prodotto> Prodotti { get; set; }

        public OrdineDTO(Ordine T)
        {
            this.UtenteId = T.UtenteId;
            this.Data_creazione = T.Data_creazione;
            this.Numero_Ordine = T.Numero_Ordine;
            this.Indirizzo_Di_Consegna = T.Indirizzo_Di_Consegna;
            this.Prezzo = T.Prezzo;
        }
    }
}
