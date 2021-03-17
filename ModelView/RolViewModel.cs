using System;
using System.ComponentModel;
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

        public RolViewModel(RolesViewModel RolesViewModel)
        {
            this.Instancia = this;
            this.RolesViewModel=RolesViewModel;

        }

        public bool CanExecute(object parametro)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if (parametro.Equals("Guardar"))
            {
                Role  NuevoRol = new Role(1,Rol);
                this.RolesViewModel.agregarElemento(NuevoRol);
                
            }
        }
    }
}