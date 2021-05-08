namespace Kalum2021.Models
{
    public class CarreraTecnica
    {
        public string Codigo_Carrera {get; set;}
        public string Nombre {get;set;}

        public CarreraTecnica(string Codigo_Carrera, string Nombre)
        {
            this.Codigo_Carrera=Codigo_Carrera;
            this.Nombre=Nombre;
            
        } 
        public CarreraTecnica()
        {
            
        }       

    }
}