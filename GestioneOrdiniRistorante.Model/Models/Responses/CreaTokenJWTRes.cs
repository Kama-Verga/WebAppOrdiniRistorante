using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.Responses
{
    public class CreaTokenJWTRes
    {
        public string Token { get; set; } = string.Empty;

        public CreaTokenJWTRes(string token)
        {
            Token = token;
        }
    }
}
