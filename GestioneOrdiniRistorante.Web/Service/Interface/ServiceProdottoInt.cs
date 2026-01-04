using GestioneOrdiniRistorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Application.Service.Interface
{
    public interface ServiceProdottoInt
    {
        Task<Prodotto> TrovaProdotto(int Id);
        Task<List<Prodotto>> Menu();
    }
}
