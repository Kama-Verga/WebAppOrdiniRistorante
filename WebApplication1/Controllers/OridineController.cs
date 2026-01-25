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
        public async Task<IActionResult> CreaOrdine(CreaOrdineReq req)
        {
            // Validazione base: se usi già [ApiController] e DataAnnotations puoi anche evitare.
            if (req == null)
                return BadRequest("Request mancante.");

            // Se UserId può essere non valido, meglio TryParse
            if (!int.TryParse(UserId, out var userId))
                return Unauthorized();

            // Qui il controller non fa business logic: delega al service
            Ordine ordineCreato = await OrdineS.CreaOrdine(req, userId);

            var response = new CreaOrdineRes
            {
                Ordine = new OrdineDTO(ordineCreato)
            };

            return Ok(response);
        }


        [HttpPost]
        [Route("Visualizza Ordine da Id")]
        public async Task<IActionResult> VisualizzaOrdine(VisualizzaOrdineReq T)
        {
            var Ris = await OrdineS.TrovaOrdine(T.Id_Ordine);
            return Ok(Ris);
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
