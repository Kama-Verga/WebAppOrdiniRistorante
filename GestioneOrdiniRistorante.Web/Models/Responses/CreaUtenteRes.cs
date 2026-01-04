using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Application.Models.DTO;

namespace GestioneOrdiniRistorante.Application.Models.Responses
{
    public class CreaUtenteRes
    {

        public UtenteDTO Utente { get; set; } = null!;

    }
}
