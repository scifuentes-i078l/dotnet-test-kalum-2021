using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class CarreraTecnicaViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public CarreraTecnicaViewModel Instancia {get;set;}

        public CarrerasTecnicasViewModel CarrerasTecnicasViewModel {get;set;}

        public string Codigo_Carrera {get;set;}

        public string Nombre {get;set;}

        public CarreraTecnica instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public CarreraTecnicaViewModel(CarrerasTecnicasViewModel CarrerasTecnicasViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.CarrerasTecnicasViewModel=CarrerasTecnicasViewModel;
            if (this.CarrerasTecnicasViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new CarreraTecnica();
                this.Codigo_Carrera=this.CarrerasTecnicasViewModel.Seleccionado.Codigo_Carrera;
                
                this.Nombre=this.CarrerasTecnicasViewModel.Seleccionado.Nombre;
                          
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

                if (this.CarrerasTecnicasViewModel.Seleccionado  == null){                    
                    CarreraTecnica nuevo = new CarreraTecnica(Codigo_Carrera,Nombre);                                        
                    this.CarrerasTecnicasViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Carrera Tecnica","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.Codigo_Carrera=this.Codigo_Carrera;
                    
                    this.instanciaTemporal.Nombre=this.Nombre;
                    
                    
                    int posicision = this.CarrerasTecnicasViewModel.Listado.IndexOf(this.CarrerasTecnicasViewModel.Seleccionado);
                    this.CarrerasTecnicasViewModel.Listado.RemoveAt(posicision);
                    this.CarrerasTecnicasViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Carrera Tecnica","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}