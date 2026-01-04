using GestioneOrdiniRistorante.Models.Models.Request;
using GestioneOrdiniRistorante.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Application.Service.Interface;
using GestioneOrdiniRistorante.Infrastructure.Repositories.Abstractions;
using System.Security.Claims;
using GestioneOrdiniRistorante.Models;

namespace GestioneOrdiniRistorante.Application.Service
{
    public class ServiceTokenJWT : ServiceTokenJWTInt
    {
        private readonly JWTAuthenticationOption jwtAuthenticationOption;
        private readonly TokenJWTRepo _tokenJWTRepository;

        public ServiceTokenJWT(IOptions<JWTAuthenticationOption> options, TokenJWTRepo tokenJWTRepository)
        {
            jwtAuthenticationOption = options.Value;
            _tokenJWTRepository = tokenJWTRepository;
        }

        private static IEnumerable<Claim> CreateClaims(Utente T)
        {
            return new List<Claim>
            {
            new(ClaimTypes.NameIdentifier, T.Id.ToString()),
            new(ClaimTypes.Name, T.Nome),
            new(ClaimTypes.Surname, T.Cognome),
            new(ClaimTypes.Email, T.Mail),
            new(ClaimTypes.Role, T.Ruolo.ToString())
            };
        }

        public string CreaToken(CreaTokenJWTReq request)
        {
            var utente = _tokenJWTRepository.GetUtente(request.Email, request.Password);

            var chiaveDiSicurezza = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthenticationOption.Key));
            var credenziali = new SigningCredentials(chiaveDiSicurezza, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = new JwtSecurityToken(jwtAuthenticationOption.Issuer
                , null
                , expires: DateTime.Now.AddMinutes(30)
                , signingCredentials: credenziali,
                claims: CreateClaims(utente)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return "Bearer " + token;

        }
    }
}
