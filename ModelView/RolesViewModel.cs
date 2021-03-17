using System.ComponentModel;
using System.Collections.ObjectModel;
using Kalum2021.Models;

using System;
using System.Windows.Input;
using System.Windows;
using Kalum2021.Views;

namespace Kalum2021.ModelView
{
    public class RolesViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public ObservableCollection<Role> Roles {get;set;}

         public RolesViewModel Instancia {get;set;}

         public Role Seleccionado {get;set;}

        public RolesViewModel()        
        {
            this.Instancia=this;
            Roles = new ObservableCollection<Role>();
            Roles.Add(new Role(1,"ROLE_ADMIN"));
            Roles.Add(new Role(2,"ROLE_USER"));
            Roles.Add(new Role(3,"ROLE_SUPERV"));            
        }
        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(Role nuevo)
        {
            this.Roles.Add(nuevo);
        }

        public bool CanExecute(object parametro)
        {
           // throw new NotImplementedException();
           return true;
        }

        public void Execute(object parametro)
        {
            if (parametro.Equals("Nuevo")){

                RolView nuevoRol= new RolView(Instancia);
                nuevoRol.Show();
                
            }else if (parametro.Equals("Eliminar")){
                if (this.Seleccionado==null){
                    MessageBox.Show("Debe seleccionar un elemento");
                }else{
                    this.Roles.Remove(Seleccionado);
                }
                
            }
        }
    }
}