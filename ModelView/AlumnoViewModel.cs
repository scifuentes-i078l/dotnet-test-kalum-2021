using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Kalum2021.ModelView
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public AlumnoViewModel Instancia {get;set;}

        private KalumDBContext dBcontext;

        public AlumnosViewModel AlumnosViewModel {get;set;}

        public string Carne {get;set;}

        public string NoExpediente {get;set;}

        public string Apellidos {get;set;}
        
        public string Nombres {get;set;}

        public string Email {get;set;}

        public Alumno AlumnoTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public String Titulo {get;set;}

        public AlumnoViewModel(AlumnosViewModel AlumnosViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.AlumnosViewModel=AlumnosViewModel;
            this.Titulo="Nuevo Registro";
            if (this.AlumnosViewModel.Seleccionado!=null)
            {
                AlumnoTemporal = new Alumno();
                this.Carne=this.AlumnosViewModel.Seleccionado.Carne;
                this.NoExpediente=this.AlumnosViewModel.Seleccionado.NoExpediente;
                this.Apellidos=this.AlumnosViewModel.Seleccionado.Apellidos;
                this.Nombres=this.AlumnosViewModel.Seleccionado.Nombres;
                this.Email=this.AlumnosViewModel.Seleccionado.Email;
                this.Titulo="Modificar Registro";
            }
            this.dBcontext= new KalumDBContext();
            
        }

        public bool CanExecute(object parametros)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if (parametro is Window)
            {
                try {
                    if (this.AlumnosViewModel.Seleccionado  == null){        
                        var ApellidosParameter = new SqlParameter("@Apellidos",this.Apellidos);
                        var NombresParameter = new SqlParameter("@Nombres",this.Nombres);
                        var EmailParameter = new SqlParameter("@Email", this.Email);
                        
                        var Resultado =this.dBcontext.Alumnos.FromSqlRaw("sp_registrar_alumno @Apellidos,@Nombres,@Email",ApellidosParameter,NombresParameter,EmailParameter).ToList();

                        foreach (Object registro in Resultado){
                               this.AlumnosViewModel.ListadoAlumnos.Add((Alumno) registro) ;
                        }
                        await dialogCoordinator.ShowMessageAsync(this,"Agregar Alumno","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);

                                 
                        
                    }else{
                        this.AlumnoTemporal.Carne=this.AlumnosViewModel.Seleccionado.Carne;
                        this.AlumnoTemporal.NoExpediente=this.NoExpediente;
                        this.AlumnoTemporal.Apellidos=this.Apellidos;
                        this.AlumnoTemporal.Nombres=this.Nombres;
                        this.AlumnoTemporal.Email=this.Email;
                        int posicision = this.AlumnosViewModel.ListadoAlumnos.IndexOf(this.AlumnosViewModel.Seleccionado);
                        this.AlumnosViewModel.ListadoAlumnos.RemoveAt(posicision);
                        this.AlumnosViewModel.ListadoAlumnos.Insert(posicision,AlumnoTemporal);
                        await dialogCoordinator.ShowMessageAsync(this,"Modificar Alumno","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                        this.dBcontext.Entry(this.AlumnoTemporal).State = EntityState.Modified;
                        this.dBcontext.SaveChanges();
                    }
                    
                    //await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","registro actualizado");
                 }catch (Exception e){
                     await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                 }
                ((Window)parametro).Close();
            }
        }
    }
}