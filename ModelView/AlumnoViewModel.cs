using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public AlumnoViewModel Instancia {get;set;}

        public AlumnosViewModel AlumnosViewModel {get;set;}

        public string Carne {get;set;}

        public string NoExpediente {get;set;}

        public string Apellidos {get;set;}
        
        public string Nombres {get;set;}

        public string Email {get;set;}

        public Alumno AlumnoTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public AlumnoViewModel(AlumnosViewModel AlumnosViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.AlumnosViewModel=AlumnosViewModel;
            if (this.AlumnosViewModel.Seleccionado!=null)
            {
                AlumnoTemporal = new Alumno();
                this.Carne=this.AlumnosViewModel.Seleccionado.Carne;
                this.NoExpediente=this.AlumnosViewModel.Seleccionado.NoExpediente;
                this.Apellidos=this.AlumnosViewModel.Seleccionado.Apellidos;
                this.Nombres=this.AlumnosViewModel.Seleccionado.Nombres;
                this.Email=this.AlumnosViewModel.Seleccionado.Email;
            }
            
        }

        public bool CanExecute(object parametros)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if (parametro is Window)
            {

                if (this.AlumnosViewModel.Seleccionado  == null){                    
                    Alumno nuevo = new Alumno(Carne,NoExpediente,Apellidos,Nombres,Email);                                        
                    this.AlumnosViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Alumno","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.AlumnoTemporal.Carne=this.Carne;
                    this.AlumnoTemporal.NoExpediente=this.NoExpediente;
                    this.AlumnoTemporal.Apellidos=this.Apellidos;
                    this.AlumnoTemporal.Nombres=this.Nombres;
                    this.AlumnoTemporal.Email=this.Email;
                    int posicision = this.AlumnosViewModel.ListadoAlumnos.IndexOf(this.AlumnosViewModel.Seleccionado);
                    this.AlumnosViewModel.ListadoAlumnos.RemoveAt(posicision);
                    this.AlumnosViewModel.ListadoAlumnos.Insert(posicision,AlumnoTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Alumno","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}