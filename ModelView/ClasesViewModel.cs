using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Kalum2021.ModelView
{
    public class ClasesViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public KalumDBContext dBContext = new KalumDBContext();

        private ObservableCollection<Clase> _Listado;
        public ObservableCollection<Clase> Listado {
            get 
            {
                if (this._Listado ==null)
                {try{
                    this._Listado= new ObservableCollection<Clase>(dBContext.Clases.Include(c => c.Instructor).Include(c => c.CarreraTecnica).Include(c => c.Salon).Include(c =>c.Horario).ToList());
                      }catch (Exception e){
                     Console.WriteLine("Error"+e.Message);
                 }
                }
                return this._Listado;
            }
            set
            {
                this._Listado=value;
            }
            }
        

    private Clase _Seleccionado;



        public Clase Seleccionado 
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

        public ClasesViewModel Instancia {get;set;}
        
        public IDialogCoordinator dialogCoordinator;

        private const string tituloVentana="Clase";

        public ClasesViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator=instanceIDialog;
        
        }

        public void agregarElemento(Clase nuevoElemento)
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
                ClaseView vistaNuevo= new ClaseView(this.Instancia);
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
                         await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Elemento Eliminado");                    
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
                    ClaseView vistaModificar = new ClaseView(Instancia);
                    vistaModificar.ShowDialog();
                }
            }
        }
    }
}