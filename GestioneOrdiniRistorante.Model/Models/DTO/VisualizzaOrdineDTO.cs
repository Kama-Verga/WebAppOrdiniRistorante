using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class VisualizzaOrdineDTO
    {
        public int NumeroOrdine { get; set; }
        public DateTime DataCreazione { get; set; }
        public string IndirizzoDiConsegna { get; set; }
        public decimal Prezzo { get; set; }
        public int UtenteId { get; set; }

        // Ecco quello che ti serve davvero
        public List<int> ProdottiId { get; set; } = new();

        public VisualizzaOrdineDTO(Ordine T, List<int> LP)
        {
            NumeroOrdine = T.Numero_Ordine;
            DataCreazione = T.Data_creazione;
            IndirizzoDiConsegna = T.Indirizzo_Di_Consegna;
            Prezzo = T.Prezzo;
            UtenteId = T.UtenteId;
            ProdottiId = LP;
        }
        public VisualizzaOrdineDTO(int NumeroOrdine, 
            DateTime DataCreazione, 
            string IndirizzoDiConsegna, 
            decimal Prezzo, int UtenteId, 
            List<int> ProdottiId)
        {
            this.NumeroOrdine = NumeroOrdine;
            this.DataCreazione = DataCreazione;
            this.IndirizzoDiConsegna = IndirizzoDiConsegna;
            this.Prezzo = Prezzo;
            this.UtenteId = UtenteId;
            this.ProdottiId = ProdottiId;
        }

    }
    
}
