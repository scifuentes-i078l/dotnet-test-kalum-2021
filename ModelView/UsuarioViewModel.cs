using System;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.Models;

namespace Kalum2021.ModelView
{
    public class UsuarioViewModel : INotifyPropertyChanged, ICommand
    {
        public UsuarioViewModel Instancia {get;set;}

        public UsuariosViewModel UsuariosViewModel {get;set;}
        public String Apellidos {get;set;}
        public String Nombres {get;set;}
        public String Email {get;set;}
        public String Username {get;set;}
        public String Password {get;set;}

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public UsuarioViewModel(UsuariosViewModel UsuariosViewModel)
        {
            this.Instancia=this;
            this.UsuariosViewModel=UsuariosViewModel;
            
        }
        public bool CanExecute(object parametros)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if (parametro.Equals("Guardar"))
            {
                Usuarios nuevo = new Usuarios(100,Username,true,Nombres,Apellidos,Email);
                this.UsuariosViewModel.agregarElemento(nuevo);
                
            }
            
        }
    }
}