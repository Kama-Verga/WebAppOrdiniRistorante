using GestioneOrdiniRistorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Application.Service.Interface
{
    public interface ServiceOrdineInt
    {
        Task<Ordine> CreaOrdine(Ordine a);
        Task<Ordine> TrovaOrdine(int Numero_Ordine);
        Task<List<Ordine>> TrovaOrdiniConUtente(DateTime Inizio, DateTime Fine, int a);
        Task<List<Ordine>> TrovaOrdini(DateTime Inizio, DateTime Fine);

    }
}
