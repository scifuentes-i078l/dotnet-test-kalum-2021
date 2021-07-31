using System.Collections.Generic;
using Kalum2021.ModelView;

namespace Kalum2021.Models
{
    public class Salon
    {
        public string SalonId {get; set;}
        public int Capacidad {get;set;}
        public string Descripcion {get;set;}
        public string NombreSalon {get;set;}

        public virtual List<Clase> Clases {get;set;}

        public Salon(string SalonId, int Capacidad, string Descripcion, string NombreSalon)
        {
            this.SalonId=SalonId;
            this.Capacidad=Capacidad;
            this.Descripcion=Descripcion;
            this.NombreSalon=NombreSalon;            
        }
        public Salon()
        {
            
        }

        public override string ToString()
        {
            return this.NombreSalon;
        }

    }
}