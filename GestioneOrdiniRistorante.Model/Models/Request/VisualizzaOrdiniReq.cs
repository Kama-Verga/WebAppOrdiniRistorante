using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.Request
{
    public class VisualizzaOrdiniReq
    {
        public DateTime GiornoInizio { get; set; }  = DateTime.Now;
        public DateTime GiornoFine { get; set; } = DateTime.Now;
        public int IdUtente_Opsionale { get; set; } = 0;
    }
}
