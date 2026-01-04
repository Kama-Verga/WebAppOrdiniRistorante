using GestioneOrdiniRistorante.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Models.Models.Request;
using GestioneOrdiniRistorante.Application.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using GestioneOrdiniRistorante.Models.Models.DTO;
using GestioneOrdiniRistorante.Models.Models.Responses;
using System.Security.Claims;


namespace GestioneOrdiniRistorante.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OridineController : Controller
    {
        private readonly ServiceOrdineInt OrdineS;
        private readonly ServiceProdottoInt ProdottoS;
        private readonly ServiceUtenteInt UtenteS;
        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string? UserRole => User.FindFirstValue(ClaimTypes.Role);

        public OridineController(ServiceOrdineInt IntTemp, ServiceProdottoInt IntTemp2, ServiceUtenteInt IntTemp3) 
        {
            OrdineS = IntTemp;
            ProdottoS = IntTemp2;
            UtenteS = IntTemp3;
        }


        [HttpPost]
        [Route("CreaOrdine")]
        public async Task<IActionResult> CreaOrdine(CreaOrdineReq T)
        {
            Ordine ordine = T.ToEntity(int.Parse(UserId));

            List<Prodotto> LP = new List<Prodotto>();

            foreach (int prodottoId in T.Contenuto)
            {
                // Recupera il prodotto
                Prodotto prodotto = await ProdottoS.TrovaProdotto(prodottoId);

                LP.Add(prodotto);

                // Aggiungi il prodotto all'ordine
                ordine.AggiungiProdotto(prodotto);
            }

            ordine.Prezzo = CalcolaTotaleConSconto(LP);

            // Salva l'ordine
            await OrdineS.CreaOrdine(ordine);

            // Crea la risposta
            var response = new CreaOrdineRes
            {
                Ordine = new OrdineDTO(ordine)
            };

            return Ok(response);
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



        [HttpPost]
        [Route("Visualizza Ordini")]
        public IActionResult ViualizzaOrdini(VisualizzaOrdiniReq T)
        {
            var Ris = new VisualizzaOrdiniRes();
            if (int.Parse(UserRole) == 0)
            {
                if (T.IdUtente_Opsionale == 0)
                {
                    Ris.Ordini = new VisualizzaOrdiniDTO(OrdineS.TrovaOrdini(T.GiornoInizio, T.GiornoFine));
                }
                else
                {
                    Ris.Ordini = new VisualizzaOrdiniDTO(OrdineS.TrovaOrdiniConUtente(T.GiornoInizio, T.GiornoFine, T.IdUtente_Opsionale));
                }
            }
            else
            { 
                if(T.IdUtente_Opsionale == 0)
                {
                    Ris.Ordini = new VisualizzaOrdiniDTO(OrdineS.TrovaOrdiniConUtente(T.GiornoInizio, T.GiornoFine, int.Parse(UserId)));
                }
                else
                {
                    throw new Exception("401 Utente non Autorizzato, non puoi curiosare nella cronologia di un utente che non sia tu");
                }            
            }
            return Ok(Ris);
        }




    }
}
