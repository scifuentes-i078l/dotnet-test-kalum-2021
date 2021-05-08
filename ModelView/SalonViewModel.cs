using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class SalonViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public SalonViewModel Instancia {get;set;}

        public SalonesViewModel SalonesViewModel {get;set;}

        public string Salon_id {get;set;}

        public int Capacidad {get;set;}

        public string Descripcion {get;set;}
        
        public string Nombre_salon {get;set;}

        public Salon instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public SalonViewModel(SalonesViewModel SalonesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.SalonesViewModel=SalonesViewModel;
            if (this.SalonesViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new Salon();
                this.Salon_id=this.SalonesViewModel.Seleccionado.Salon_id;
                this.Capacidad=this.SalonesViewModel.Seleccionado.Capacidad;
                this.Descripcion=this.SalonesViewModel.Seleccionado.Descripcion;
                this.Nombre_salon=this.SalonesViewModel.Seleccionado.Nombre_salon;             
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

                if (this.SalonesViewModel.Seleccionado  == null){                    
                    Salon nuevo = new Salon(Salon_id,Capacidad,Descripcion,Nombre_salon);                                        
                    this.SalonesViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Salon","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.Salon_id=this.Salon_id;
                    this.instanciaTemporal.Capacidad=this.Capacidad;
                    this.instanciaTemporal.Descripcion=this.Descripcion;
                    this.instanciaTemporal.Nombre_salon=this.Nombre_salon;
                    
                    int posicision = this.SalonesViewModel.Listado.IndexOf(this.SalonesViewModel.Seleccionado);
                    this.SalonesViewModel.Listado.RemoveAt(posicision);
                    this.SalonesViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Salon","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}