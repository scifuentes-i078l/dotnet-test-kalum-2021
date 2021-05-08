using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class AlumnosViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<Alumno> ListadoAlumnos {get;set;}
        public Alumno Seleccionado {get;set;}

        public AlumnosViewModel Instancia {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public AlumnosViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator= instanceIDialog;
            this.ListadoAlumnos = new ObservableCollection<Alumno>();
            this.ListadoAlumnos.Add (new Alumno("1","exp1","Apellidos1","Alumno1","email1@kinal.edu"));
            this.ListadoAlumnos.Add (new Alumno("2","exp2","Apellidos2","Alumno2","email2@kinal.edu"));
            this.ListadoAlumnos.Add (new Alumno("3","exp3","Apellidos3","Alumno3","email3@kinal.edu"));
        }

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(Alumno nuevoAlumno) 
        {
            this.ListadoAlumnos.Add(nuevoAlumno);
        }

        public bool CanExecute(object parametro)
        {
             return true;
        }

        public async void Execute(object parametro)
        {
            if (parametro.Equals("Nuevo"))
            {
                this.Seleccionado=null;
                AlumnoView nuevoUsuario= new AlumnoView(Instancia);
                nuevoUsuario.Show();                                
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado==null)
                {

                    await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar alumno","Esta seguro de Eliminar el registro?",
                    MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta==MessageDialogResult.Affirmative) 
                    {
                         this.ListadoAlumnos.Remove(Seleccionado);
                    }
                }
                
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.Seleccionado== null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    AlumnoView modificarUsuario = new AlumnoView(Instancia);
                    modificarUsuario.ShowDialog();
                }
            }
        }
    }
}