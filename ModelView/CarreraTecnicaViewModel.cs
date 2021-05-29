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
    public class CarreraTecnicaViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public CarreraTecnicaViewModel Instancia {get;set;}

        public CarrerasTecnicasViewModel CarrerasTecnicasViewModel {get;set;}

        public string CarreraId {get;set;}

        public string Nombre {get;set;}

        public CarreraTecnica instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        private KalumDBContext dBcontext;

        public String Titulo {get;set;}

        public CarreraTecnicaViewModel(CarrerasTecnicasViewModel CarrerasTecnicasViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.CarrerasTecnicasViewModel=CarrerasTecnicasViewModel;
            this.Titulo="Nuevo Registro";
            if (this.CarrerasTecnicasViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new CarreraTecnica();
                this.CarreraId=this.CarrerasTecnicasViewModel.Seleccionado.CarreraId;
                
                this.Nombre=this.CarrerasTecnicasViewModel.Seleccionado.Nombre;
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
                if (this.CarrerasTecnicasViewModel.Seleccionado  == null){                    
                    CarreraTecnica nuevo = new CarreraTecnica(CarreraId,Nombre);                                        
                    this.CarrerasTecnicasViewModel.agregarElemento(nuevo);
                    this.dBcontext.CarrerasTecnicas.Add(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Carrera Tecnica","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.CarreraId=this.CarrerasTecnicasViewModel.Seleccionado.CarreraId;
                    
                    this.instanciaTemporal.Nombre=this.Nombre;
                    
                    
                    int posicision = this.CarrerasTecnicasViewModel.Listado.IndexOf(this.CarrerasTecnicasViewModel.Seleccionado);
                    this.CarrerasTecnicasViewModel.Listado.RemoveAt(posicision);
                    this.CarrerasTecnicasViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Carrera Tecnica","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
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