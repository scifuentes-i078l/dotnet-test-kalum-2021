using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace Kalum2021.ModelView
{
    public class SalonViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public SalonViewModel Instancia {get;set;}

        public SalonesViewModel SalonesViewModel {get;set;}

        public string SalonId {get;set;}

        public int Capacidad {get;set;}

        public string Descripcion {get;set;}
        
        public string NombreSalon {get;set;}

        public Salon instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        private KalumDBContext dBcontext;

        public String Titulo {get;set;}

        public SalonViewModel(SalonesViewModel SalonesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.SalonesViewModel=SalonesViewModel;
            this.Titulo="Nuevo Registro";
            if (this.SalonesViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new Salon();
                this.SalonId=this.SalonesViewModel.Seleccionado.SalonId;
                this.Capacidad=this.SalonesViewModel.Seleccionado.Capacidad;
                this.Descripcion=this.SalonesViewModel.Seleccionado.Descripcion;
                this.NombreSalon=this.SalonesViewModel.Seleccionado.NombreSalon;  
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
                if (this.SalonesViewModel.Seleccionado  == null){                    
                    Salon nuevo = new Salon(Guid.NewGuid().ToString(),Capacidad,Descripcion,NombreSalon);                                        
                    this.SalonesViewModel.agregarElemento(nuevo);
                     this.dBcontext.Salones.Add(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Salon","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.SalonId=this.SalonesViewModel.Seleccionado.SalonId;
                    this.instanciaTemporal.Capacidad=this.Capacidad;
                    this.instanciaTemporal.Descripcion=this.Descripcion;
                    this.instanciaTemporal.NombreSalon=this.NombreSalon;
                    
                    int posicision = this.SalonesViewModel.Listado.IndexOf(this.SalonesViewModel.Seleccionado);
                    this.SalonesViewModel.Listado.RemoveAt(posicision);
                    this.SalonesViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Salon","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                    this.dBcontext.Entry(this.instanciaTemporal).State = EntityState.Modified;
                }
                this.dBcontext.SaveChanges();
                ((Window)parametro).Close();
                 }catch (Exception e){
                     await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                 }
            }
        }
    }
}