using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Models.DTO;
using GestioneOrdiniRistorante.Models.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Application.Service.Interface
{
    public interface ServiceOrdineInt
    {
        Task<Ordine> CreaOrdine(CreaOrdineReq req, int userId);
        Task<VisualizzaOrdineDTO> TrovaOrdine(int numeroOrdine);
        Task<List<Ordine>> TrovaOrdiniConUtente(DateTime Inizio, DateTime Fine, int a);
        Task<List<Ordine>> TrovaOrdini(DateTime Inizio, DateTime Fine);

    }
}
