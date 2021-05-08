namespace Kalum2021.Models
{

   
        
    public class Instructor
    {

    public string Instructor_id {get; set;}
    public string  Nombres {get; set;}
    public string Apellidos {get; set;}
    public string  Comentario {get; set;}
    public string  Direccion {get; set;}
    public string  Estatus {get; set;}
    public string  Foto {get; set;}
    public string Telefono {get; set;}

    public Instructor(string Instructor_id, string  Nombres, string Apellidos,string  Comentario,string  Direccion, string  Estatus,string  Foto,string Telefono)
    {
        this.Instructor_id=Instructor_id;
        this.Nombres=Nombres;
        this.Apellidos=Apellidos;
        this.Comentario=Comentario;
        this.Direccion=Direccion;
        this.Estatus=Estatus;
        this.Foto=Foto;
        this.Telefono=Telefono;
    }

    public Instructor()
    {
        
    }
        
    }
}