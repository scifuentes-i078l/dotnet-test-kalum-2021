using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;

namespace Kalum2021.ModelView
{
    public class RolViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public RolViewModel Instancia {get;set;}

        public RolesViewModel RolesViewModel {get;set;}

        public String Rol {get;set;}

        public Role RolModificado {get;set;}

        public RolViewModel(RolesViewModel RolesViewModel)
        {
            this.Instancia = this;
            this.RolesViewModel=RolesViewModel;
            if (this.RolesViewModel.Seleccionado!=null){
                this.RolModificado = new Role();
                this.RolModificado.Id=this.RolesViewModel.Seleccionado.Id;
                this.Rol= this.RolesViewModel.Seleccionado.Nombre;                
            }

        }

        public bool CanExecute(object parametro)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if (parametro is Window)
            {


                if (this.RolesViewModel.Seleccionado ==null)
                {
                    Role  NuevoRol = new Role(1,Rol);
                    this.RolesViewModel.agregarElemento(NuevoRol);
                    
                }else
                {
                    RolModificado.Nombre=this.Rol;
                    int posicion = this.RolesViewModel.Roles.IndexOf(this.RolesViewModel.Seleccionado);
                    this.RolesViewModel.Roles.RemoveAt(posicion);
                    this.RolesViewModel.Roles.Insert(posicion,RolModificado);
                }
                ((Window)parametro).Close();


                
                
            }
        }
    }
}