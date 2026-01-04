using GestioneOrdiniRistorante.Models;
using GestioneOrdiniRistorante.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Models.Models.Request;
using GestioneOrdiniRistorante.Models.Models.Responses;
using GestioneOrdiniRistorante.Models.Models.DTO;
using GestioneOrdiniRistorante.Application.Service;

namespace GestioneOrdiniRistorante.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtenteController : Controller
    {
        private readonly ServiceUtenteInt UtenteS;

        public UtenteController(ServiceUtenteInt UsI)
        {
            UtenteS = UsI;
        }

        [HttpPost]
        [Route("Crea-Utente")]  // Nome specifico per questo POST
        // 0 = Utente - 1 = amministratore
        public async Task<IActionResult> CreaUtente(CreaUtenteReq T)
        {
            Utente Tr = await UtenteS.CreaUtente(T.ToEntity());


            var Ris = new CreaUtenteRes();
            Ris.Utente = new UtenteDTO(Tr);
            return Ok(Ris);
        }


    }
}
