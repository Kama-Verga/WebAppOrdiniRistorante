using GestioneOrdiniRistorante.Models;
using System.ComponentModel.Design;
using GestioneOrdiniRistorante.Infrastructure;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;
using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Models.Models.DTO;

namespace GestioneOrdiniRistorante.Service
{
    public class ServiceOrdine : ServiceOrdineInt
    {

        private readonly OrdineRepo OrdineDB;

        public ServiceOrdine(OrdineRepo OR)
        {
            OrdineDB = OR;
        }
        public async Task<Ordine> CreaOrdine(Ordine ordine)
        {
            OrdineDB.AddAsync(ordine); // Aggiunge l'ordine
            await OrdineDB.SalvaDatiAsync(); // Salva solo una volta al termine
            Console.WriteLine(ordine.UtenteId);
            return ordine;
        }


        public async Task<List<Ordine>> TrovaOrdiniConUtente(DateTime Inizio, DateTime Fine, int IdUtente)
        {
            var T = await OrdineDB.TrovaOrdiniConUtente(Inizio, Fine, IdUtente );
            Console.WriteLine("fatto");
            return T;
        }

        public async Task<List<Ordine>> TrovaOrdini(DateTime Inizio, DateTime Fine)
        {
            var T = await OrdineDB.TrovaOrdini(Inizio, Fine);
            Console.WriteLine("fatto");
            return T;
        }
    }
}
