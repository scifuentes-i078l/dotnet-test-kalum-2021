namespace Kalum2021.Models
{
    public class CarreraTecnica
    {
        public string CarreraId {get; set;}
        public string Nombre {get;set;}

        public CarreraTecnica(string CarreraId, string Nombre)
        {
            this.CarreraId=CarreraId;
            this.Nombre=Nombre;
            
        } 
        public CarreraTecnica()
        {
            
        }       

    }
}