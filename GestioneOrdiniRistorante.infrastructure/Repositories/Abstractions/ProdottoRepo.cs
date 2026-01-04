using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions
{
    public class ProdottoRepo : GenericRepository<Prodotto>
    {
        public MyDBContent DB;

        public ProdottoRepo(MyDBContent T) : base(T)
        {
            DB = T;
        }

        public async Task<List<Prodotto>> Menu()
        {
            var query = await DB.Prodotto.ToListAsync();
            return query;
        }


        public void Add(Prodotto T)
        {
            DB.Add(T);
        }

        public async Task<Prodotto> FindById(int i)
        {
            var query = await DB.Set<Prodotto>().FindAsync(i);
            return query;
        }

    }
}
