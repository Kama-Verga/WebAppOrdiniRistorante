using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class VisualizzaOrdiniDTO
    {
        public Task<List<Ordine>> ordine { get; set; }

        public VisualizzaOrdiniDTO(Task<List<Ordine>> T)
        {
            ordine = T;
        }
    }
    
}
