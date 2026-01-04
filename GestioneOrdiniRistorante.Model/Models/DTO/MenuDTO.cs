using GestioneOrdiniRistorante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdiniRistorante.Models.Models.DTO
{
    public class MenuDTO
    {
        public Task<List<Prodotto>> Menu {  get; set; }

        public MenuDTO(Task<List<Prodotto>> T) {
            
            Menu = T;
        
        }


    }
}
