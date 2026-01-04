using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;

namespace GestioneOrdiniRistorante.Application.Service
{
    public class ServiceProdotto : ServiceProdottoInt
    {
        ProdottoRepo ProdottoDB;
        public ServiceProdotto(ProdottoRepo PR) 
        {
            ProdottoDB = PR;
        }

        public async Task<Prodotto> TrovaProdotto(int Id)
        {
            var T = await ProdottoDB.FindById(Id);
            return T;
        }
        
        public async Task<List<Prodotto>> Menu()
        {
            var T = await ProdottoDB.Menu();
            await ProdottoDB.SalvaDatiAsync();
            return T;
        }


    }
}
