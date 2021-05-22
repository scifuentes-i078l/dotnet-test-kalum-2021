namespace Kalum2021.ModelView
{
    public class Clase
    {
        public string ClaseId {get; set;}

        public string Descripcion {get; set;}

        public int Ciclo {get; set;}
        public int CupoMaximo {get; set;}
        public int CupoMinimo {get; set;}

        public string CarreraId {get; set;}
        public string HorarioId {get; set;}
        public string InstructorId {get; set;}
        public string SalonId {get; set;}

        
        public Clase(string ClaseId, string Descripcion, int Ciclo, int CupoMaximo, int CupoMinimo, string CarreraId, string HorarioId, string InstructorId, string SalonId)
        {
            this.ClaseId=ClaseId;
            this.Descripcion=Descripcion;
            this.Ciclo=Ciclo;
            this.CupoMaximo=CupoMaximo;
            this.CupoMinimo=CupoMinimo;
            this.CarreraId=CarreraId;
            this.HorarioId=HorarioId;
            this.InstructorId=InstructorId;
            this.SalonId=SalonId;
            
        }

        public Clase()
        {
            
        }
        
    }
}