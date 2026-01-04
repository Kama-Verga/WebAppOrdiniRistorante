using GestioneOrdiniRistorante.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using GestioneOrdiniRistorante.Models.Models.Request;
using GestioneOrdiniRistorante.Models.Models.Responses;
using GestioneOrdiniRistorante.Models.Models.DTO;
using GestioneOrdiniRistorante.Application.Service;
using System.ComponentModel.Design;

namespace GestioneOrdiniRistorante.Web.Controllers
{
    public class ProdottoController : Controller
    {
        private readonly ServiceProdottoInt ProdottoS;

        public ProdottoController(ServiceProdottoInt PsI)
        {
            ProdottoS = PsI;
        }

        [HttpGet]
        [Route("Menù")]
        public IActionResult PostMenu()
        {
            var Ris = new MenuRes();
            Ris.Menu = new MenuDTO(ProdottoS.Menu());
            return Ok(Ris);
        }
    }
}
