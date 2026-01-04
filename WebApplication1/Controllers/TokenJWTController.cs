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

namespace GestioneOrdiniRistorante.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : Controller
    {
        private readonly ServiceTokenJWTInt TokenJWTS;

        public TokenController(ServiceTokenJWTInt TJWTS)
        {
            this.TokenJWTS = TJWTS;
        }

        [HttpPost]
        [Route("Create Token")]
        public IActionResult CreateToken(CreaTokenJWTReq request)
        {
            string token = TokenJWTS.CreaToken(request);
            return Ok(new CreaTokenJWTRes(token));
        }
    }
}
