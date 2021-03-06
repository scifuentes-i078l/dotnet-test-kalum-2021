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
    public class InstructoresViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
       public KalumDBContext dBContext = new KalumDBContext();

        private ObservableCollection<Instructor> _Listado;
        public ObservableCollection<Instructor> Listado {
            get
            {
                if (this._Listado ==null)
                {
                    this._Listado= new ObservableCollection<Instructor>(dBContext.Instructores.ToList());
                }
                return this._Listado;
            }
            set
            {
                this._Listado=value;
            }
            }
        private Instructor _Seleccionado;
        public Instructor Seleccionado 
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

        public void NotificarCambio(String Propiedad){
            if (PropertyChanged!=null){
                PropertyChanged(this, new PropertyChangedEventArgs(Propiedad));
            }
        }

        public InstructoresViewModel Instancia {get;set;}
        
        public IDialogCoordinator dialogCoordinator;

        private const string tituloVentana="Instructor";

        public InstructoresViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator=instanceIDialog;            
        }

        public void agregarElemento(Instructor nuevoElemento)
        {
            this.Listado.Add(nuevoElemento);
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
                InstructorView vistaNuevo= new InstructorView(Instancia);
                vistaNuevo.Show();                                
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado==null)
                {

                    await this.dialogCoordinator.ShowMessageAsync(this,tituloVentana,"Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    tituloVentana,"Esta seguro de Eliminar el registro?",
                    MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta==MessageDialogResult.Affirmative) 
                    {
                         try{
                        int posicion = this.Listado.IndexOf(this.Seleccionado);
                         this.dBContext.Remove(this.Seleccionado);
                         this.dBContext.SaveChanges();
                         this.Listado.RemoveAt(posicion);
                         await this.dialogCoordinator.ShowMessageAsync(this,"Instructores","Elemento Eliminado");                    
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
                    await this.dialogCoordinator.ShowMessageAsync(this,tituloVentana,"Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    InstructorView vistaModificar = new InstructorView(Instancia);
                    vistaModificar.ShowDialog();
                }
            }
        }
    }
}