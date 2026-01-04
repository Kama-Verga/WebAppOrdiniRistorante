using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneOrdiniRistorante.Models.Models.Request;

namespace GestioneOrdiniRistorante.Application.Service.Interface
{
    public interface ServiceTokenJWTInt
    {
        string CreaToken(CreaTokenJWTReq Req);
    }
}
