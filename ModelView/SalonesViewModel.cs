using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class SalonesViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ObservableCollection<Salon> Listado {get;set;}
        public Salon Seleccionado {get;set;}

        public SalonesViewModel Instancia {get;set;}
        
        public IDialogCoordinator dialogCoordinator;

        private const string tituloVentana="Salon";

        public SalonesViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator=instanceIDialog;
            this.Listado= new ObservableCollection<Salon>();
            this.Listado.Add(new Salon("1",1,"salon 1","salon 1"));            
        }

        public void agregarElemento(Salon nuevoElemento)
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
                SalonView vistaNuevo= new SalonView(Instancia);
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
                         this.Listado.Remove(Seleccionado);
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
                    SalonView vistaModificar = new SalonView(Instancia);
                    vistaModificar.ShowDialog();
                }
            }
        }
    }
}