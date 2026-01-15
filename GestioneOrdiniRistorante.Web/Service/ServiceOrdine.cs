using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Infrastructure;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Entities;
using GestioneOrdiniRistorante.Models.Models.DTO;
using System.ComponentModel.Design;

namespace GestioneOrdiniRistorante.Service
{
    public class ServiceOrdine : ServiceOrdineInt
    {

        private readonly OrdineRepo OrdineDB;
        private readonly ProdottoInOrdineRepo ProdottoInOrdineDB;

        public ServiceOrdine(OrdineRepo or, ProdottoInOrdineRepo pio)
        {
            OrdineDB = or;
            ProdottoInOrdineDB = pio;
        }
        public async Task<Ordine> CreaOrdine(Ordine ordine)
        {
            OrdineDB.AddAsync(ordine); // Aggiunge l'ordine
            await OrdineDB.SalvaDatiAsync(); // Salva solo una volta al termine
            Console.WriteLine(ordine.UtenteId);
            return ordine;
        }

        // Trova ordine + lista id prodotti (DTO)
        public async Task<VisualizzaOrdineDTO> TrovaOrdine(int numeroOrdine)
        {
            var ordine = await OrdineDB.TrovaOrdine(numeroOrdine);
            if (ordine == null) return null;

            var prodottiId = await ProdottoInOrdineDB.TrovaProdottiIdPerOrdine(numeroOrdine);

            // Qui assumo che tu abbia aggiornato VisualizzaOrdineDTO in forma "piatta":
            // NumeroOrdine, DataCreazione, IndirizzoDiConsegna, Prezzo, UtenteId, ProdottiId
            var dto = new VisualizzaOrdineDTO(
                ordine.Numero_Ordine,
                ordine.Data_creazione,
                ordine.Indirizzo_Di_Consegna,
                ordine.Prezzo,
                ordine.UtenteId,
                prodottiId);

            return dto;
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
