using GestioneOrdiniRistorante.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions
{
    public class OrdineRepo : GenericRepository<Ordine>
    {
        public MyDBContent DB;

        public OrdineRepo(MyDBContent T) : base(T)
        {
            DB = T;
        }

        public void Add(Ordine T)
        {
            DB.Add(T);
        }

        public void AddAsync(Ordine T)
        {
            DB.AddAsync(T);
        }

        public async Task<List<Ordine>> TrovaOrdiniConUtente(DateTime dataInizio, DateTime dataFine, int IdUtente)
        {
            return await DB.Ordine
                .Where(o => o.Data_creazione >= dataInizio && o.Data_creazione <= dataFine && o.UtenteId == IdUtente)
                .ToListAsync();
        }
        public async Task<List<Ordine>> TrovaOrdini(DateTime dataInizio, DateTime dataFine)
        {
            return await DB.Ordine
                .Where(o => o.Data_creazione >= dataInizio && o.Data_creazione <= dataFine)
                .ToListAsync();
        }

        public async Task<Ordine> TrovaOrdine(int Numero_Ordine)
        {
            return await DB.Ordine
                .FirstOrDefaultAsync(o => o.Numero_Ordine == Numero_Ordine);
        }
    }
}
