using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Infrastructure;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Entities;
using GestioneOrdiniRistorante.Models.Models.DTO;
using GestioneOrdiniRistorante.Models.Models.Request;
using System.ComponentModel.Design;

namespace GestioneOrdiniRistorante.Service
{
    public class ServiceOrdine : ServiceOrdineInt
    {

        private readonly OrdineRepo OrdineDB;
        private readonly ServiceProdottoInt ProdottoS;
        private readonly ProdottoInOrdineRepo ProdottoInOrdineDB;

        public ServiceOrdine(OrdineRepo or, ProdottoInOrdineRepo pio, ServiceProdottoInt IntTemp2)
        {
            OrdineDB = or;
            ProdottoInOrdineDB = pio;
            ProdottoS = IntTemp2;
        }
        public async Task<Ordine> CreaOrdine(CreaOrdineReq req, int userId)
        {
            Ordine ordine = req.ToEntity(userId);

            List<Prodotto> prodotti = new List<Prodotto>();

            foreach (int prodottoId in req.Contenuto)
            {
                Prodotto prodotto = await ProdottoS.TrovaProdotto(prodottoId);

                // Se TrovaProdotto può tornare null, qui va gestito (dimmi come vuoi gestirlo: 404? 400?)
                // if (prodotto == null) throw ...

                prodotti.Add(prodotto);
                ordine.AggiungiProdotto(prodotto);
            }

            ordine.Prezzo = CalcolaTotaleConSconto(prodotti);

            // SALVATAGGIO: identico al tuo service
            OrdineDB.AddAsync(ordine);
            await OrdineDB.SalvaDatiAsync();

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


        public static decimal CalcolaTotaleConSconto(List<Prodotto> prodotti)
        {
            decimal totale = 0;

            // Raggruppiamo i prodotti per tipo
            var gruppi = prodotti.GroupBy(p => p.Tipo);

            // Per ogni gruppo applicare lo sconto del 10%
            foreach (var gruppo in gruppi)
            {
                // Calcoliamo la somma del gruppo
                decimal sommaGruppo = gruppo.Sum(p => p.Prezzo);

                // Applichiamo lo sconto del 10% al gruppo
                decimal sommaScontata = sommaGruppo * 0.9m;

                // Aggiungiamo al totale
                totale += sommaScontata;
            }

            return totale;
        }

    }
}
