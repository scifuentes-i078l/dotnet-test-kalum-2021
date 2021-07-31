using System.Collections.Generic;

namespace Kalum2021.Models
{
    public class Modulo
    {
        public string ModuloId {get;set;}

        public string CarreraId {get;set;}

        public string NombreModulo {get;set;}

        public int NumeroSeminario {get;set;}

        public virtual CarreraTecnica CarreraTecnica {get;set;}

        public virtual List<Seminario> Seminarios {get;set;}
        public Modulo()
        {
            
        }

    }
}