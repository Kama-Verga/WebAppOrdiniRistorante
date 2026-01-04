using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using GestioneOrdiniRistorante.Models.Entities;

namespace GestioneOrdiniRistorante.Models
{
    public class Ordine
    {
    
        [Key]
        public int Numero_Ordine;
        public int UtenteId { get; set; }  // Chiave esterna
        public DateTime Data_creazione { get; set; }
        public String Indirizzo_Di_Consegna { get; set; }
        public decimal Prezzo { get; set; }

        // Relazione molti-a-molti con OrdineProdotto
        public ICollection<ProdottoInOrdine> ProdottiInOrdine { get; set; } = new List<ProdottoInOrdine>();

        public Ordine(int UtenteId, String Indirizzo_Di_Consegna)
        {
            this.UtenteId = UtenteId;
            this.Data_creazione = DateTime.Now.Date;
            this.Indirizzo_Di_Consegna = Indirizzo_Di_Consegna;
        }
        public void AggiungiProdotto(Prodotto T)
        {
            ProdottiInOrdine.Add(new ProdottoInOrdine(this, T));
        }
    
    }
}
