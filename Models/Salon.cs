namespace Kalum2021.Models
{
    public class Salon
    {
        public string Salon_id {get; set;}
        public int Capacidad {get;set;}
        public string Descripcion {get;set;}
        public string Nombre_salon {get;set;}

        public Salon(string Salon_id, int Capacidad, string Descripcion, string Nombre_salon)
        {
            this.Salon_id=Salon_id;
            this.Capacidad=Capacidad;
            this.Descripcion=Descripcion;
            this.Nombre_salon=Nombre_salon;            
        }
        public Salon()
        {
            
        }

    }
}