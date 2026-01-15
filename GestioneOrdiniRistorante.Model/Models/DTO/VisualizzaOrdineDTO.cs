using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class VisualizzaOrdineDTO
    {
        public Ordine ordine { get; set; }

        public VisualizzaOrdineDTO(Ordine T)
        {
            ordine = T;
        }

    }
    
}
