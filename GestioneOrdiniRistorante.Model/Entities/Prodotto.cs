using GestioneOrdiniRistorante.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace GestioneOrdiniRistorante.Models
{
    public class Prodotto
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; set; }
        
        public Decimal Prezzo { get; set; }
        public int Tipo { get; set; } //(0 primo - 1 secondo - 2 contorno - 3 dolce)

        public ICollection<ProdottoInOrdine> ProdottiInOrdine { get; set; }

        public Prodotto(int Id, String Nome, Decimal Prezzo, int Tipo)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Prezzo = Prezzo;
            this.Tipo = Tipo;
        }

        public void AggiungiOrdine(Ordine T)
        {
            ProdottoInOrdine PIT = new ProdottoInOrdine(T, this);
        }
    }


}
