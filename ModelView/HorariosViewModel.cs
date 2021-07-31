using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using Kalum2021.Views;

namespace Kalum2021.ModelView
{
    public class HorariosViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;


         public KalumDBContext dBContext = new KalumDBContext();

        private ObservableCollection<Horario> _Listado;
        public ObservableCollection<Horario> Listado {
            get
            {
                if (this._Listado ==null)
                {
                    this._Listado= new ObservableCollection<Horario>(dBContext.Horarios.ToList());
                }
                return this._Listado;
            }
            set
            {
                this._Listado=value;
            }
            }
        
        private Horario _Seleccionado;
        public Horario Seleccionado 
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

        public bool CanExecute(object parameter)
        {
             return true;
        }

        public HorariosViewModel Instancia {get;set;}
        
        public IDialogCoordinator dialogCoordinator;

        private const string tituloVentana="Horario";

        public HorariosViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator=instanceIDialog;
        
        }

        public void agregarElemento(Horario nuevoElemento)
        {
            this.Listado.Add(nuevoElemento);
        }
       public async void Execute(object parametro)
        {
            if (parametro.Equals("Nuevo"))
            {
                this.Seleccionado=null;
                HorarioView vistaNuevo= new HorarioView(Instancia);
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
                         await this.dialogCoordinator.ShowMessageAsync(this,"Horarios","Elemento Eliminado");                    
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
                    HorarioView vistaModificar = new HorarioView(Instancia);
                    vistaModificar.ShowDialog();
                }
            }
        }




    }
}