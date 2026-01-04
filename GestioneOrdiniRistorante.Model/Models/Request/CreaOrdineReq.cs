using System.Data;
using System.Text.Json.Serialization;

namespace GestioneOrdiniRistorante.Models.Models.Request
{
    public class CreaOrdineReq
    {
        public String Indirizzo_Di_Consegna { get; set; } = string.Empty;
        public List<int> Contenuto { get; set; }
        public Ordine ToEntity(int Id)
        {
            Ordine ordine = new Ordine(Id, Indirizzo_Di_Consegna);

            return ordine;
        }
    }
}
