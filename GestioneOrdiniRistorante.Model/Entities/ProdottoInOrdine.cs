using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Entities
{
    public class ProdottoInOrdine
    {
        public int OrdineId { get; set; }

        [JsonIgnore]
        public Ordine Ordine { get; set; }
        public int Id { get; set; }
        public int ProdottoId { get; set; }

        [JsonIgnore]
        public Prodotto Prodotto { get; set; }

        public ProdottoInOrdine() { }
        public ProdottoInOrdine(Ordine T, Prodotto TT ) { 
            Ordine = T;
            Prodotto = TT;
        }
    }
}
