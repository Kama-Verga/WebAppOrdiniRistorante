using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions
{
    public class ProdottoInOrdineRepo : GenericRepository<ProdottoInOrdine>
    {
        public MyDBContent DB;

        public ProdottoInOrdineRepo(MyDBContent t) : base(t)
        {
            DB = t;
        }

        public void Add(ProdottoInOrdine t)
        {
            DB.Add(t);
        }

        public void AddAsync(ProdottoInOrdine t)
        {
            DB.AddAsync(t);
        }

        // Ritorna tutte le righe della tabella ponte per un ordine
        public async Task<List<ProdottoInOrdine>> TrovaPerOrdine(int ordineId)
        {
            return await DB.Set<ProdottoInOrdine>()
                .Where(pio => pio.OrdineId == ordineId)
                .ToListAsync();
        }

        // Quello che ti serve davvero: lista di ID dei prodotti per un ordine
        public async Task<List<int>> TrovaProdottiIdPerOrdine(int ordineId)
        {
            return await DB.Set<ProdottoInOrdine>()
                .Where(pio => pio.OrdineId == ordineId)
                .Select(pio => pio.ProdottoId)
                .Distinct()
                .ToListAsync();
        }
    }
}
