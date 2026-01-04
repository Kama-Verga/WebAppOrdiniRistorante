using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions
{
    public abstract class GenericRepository<T> where T : class
    {
        protected MyDBContent _ctx;

        public GenericRepository(MyDBContent ctx)
        {
            _ctx = ctx;
        }

        public void Add(T entity)
        {
            _ctx.Add(entity);
        }

        public void Update(T entity)
        {
            _ctx.Update(entity);
        }

        public void Delete(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public T? Get(object id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public void Save()
        {
            _ctx.SaveChanges();
            /*
            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("DbUpdateException error details: " + ex.InnerException?.Message);
                throw;  // Rilancia l'eccezione se necessario
            }
            */
        }

        public async Task SalvaDatiAsync()
        {
            try
            {

                Console.WriteLine("salvataggio in corso");
                await _ctx.SaveChangesAsync();
                Console.WriteLine("salvataggio completato");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Errore durante il salvataggio:");
                Console.WriteLine(ex.InnerException?.Message); // Dettaglio principale
                Console.WriteLine(ex.InnerException?.InnerException?.Message); // Causa radice (se presente)
                throw; // Rilancia l'eccezione se necessario
            }
        }



    }
}


