

using System.ComponentModel.DataAnnotations;

namespace Kalum2021.Models
{
    public class Alumno
    {
        public string Carne {get;set;}
        public string NoExpediente {get;set;}

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos {get;set;}

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres {get;set;}
       [Required(ErrorMessage = "El campo {0} es requerido")]
       [EmailAddress]
        public string Email {get;set;}

        public Alumno(string Carne, string NoExpediente, string Apellidos, string Nombres, string Email)
        {
            this.Carne=Carne;
            this.NoExpediente=NoExpediente;
            this.Apellidos=Apellidos;
            this.Nombres=Nombres;
            this.Email=Email;
        }
        
        public Alumno()
        {
            
        }
    }
}