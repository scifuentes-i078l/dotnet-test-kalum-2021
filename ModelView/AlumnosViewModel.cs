using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;

namespace Kalum2021.ModelView
{
    public class AlumnosViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Alumno> _ListadoAlumnos;
        public ObservableCollection<Alumno> ListadoAlumnos {
            get
            {
                if (this._ListadoAlumnos ==null)
                {
                    this._ListadoAlumnos= new ObservableCollection<Alumno>(dBContext.Alumnos.ToList());
                }
                return this._ListadoAlumnos;
            }
            set
            {
                this._ListadoAlumnos=value;
            }
            }
         private Alumno _Seleccionado;
        public Alumno Seleccionado 
        {
            get
            {
                return this._Seleccionado;

            }
            
            set
            {
                this._Seleccionado=value;
                NotificarCambio("Seleccionado");

            }
        
        }

        public AlumnosViewModel Instancia {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public KalumDBContext dBContext = new KalumDBContext();

        public AlumnosViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator= instanceIDialog;
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
                        try{
                        int posicion = this.ListadoAlumnos.IndexOf(this.Seleccionado);
                        // this.ListadoAlumnos.Remove(this.Seleccionado);
                         this.dBContext.Remove(this.Seleccionado);
                         this.dBContext.SaveChanges();
                         this.ListadoAlumnos.RemoveAt(posicion);
                         await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Elemento Eliminado");                    
                        }catch (Exception e){
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);

                        }
                        
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