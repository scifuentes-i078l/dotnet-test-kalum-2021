

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class UsuariosViewModel: INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Usuarios> Usuarios {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;

        private IDialogCoordinator dialogCoordinator;
        public UsuariosViewModel Instancia {get;set;}

        public Usuarios Seleccionado {get;set;}

        public event EventHandler CanExecuteChanged;

        public UsuariosViewModel(IDialogCoordinator instance)
        {
            this.Instancia= this;
            this.dialogCoordinator= instance;
            this.Usuarios= new ObservableCollection<Usuarios>();
            this.Usuarios.Add(new Usuarios(1,"etumax",true,"Edwin Rolando","Tumax Chaclan", "etumax@gmail.com"));
            this.Usuarios.Add(new Usuarios(2,"nperez",true,"Nancy Elizabeth","Perez Carcamo", "eperez@gmail.com"));
            this.Usuarios.Add(new Usuarios(3,"caquino",true,"Cesar Augusto","Aquino Gaitan", "caquino@gmail.com"));
        }

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }


        public void agregarElemento(Usuarios nuevo) 
        {
            this.Usuarios.Add(nuevo);
        }
        public bool CanExecute(object parametro)
        {
                //throw new NotImplementedException();
            return true;
        }

        public async void Execute(object parametro)
        {
            if (parametro.Equals("Nuevo"))
            {
                this.Seleccionado=null;
                UsuarioView nuevoUsuario= new UsuarioView(Instancia);
                nuevoUsuario.Show();                                
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado==null)
                {

                    await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar usuario","Esta seguro de Eliminar el registro?",
                    MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta==MessageDialogResult.Affirmative) 
                    {
                         this.Usuarios.Remove(Seleccionado);
                    }
                }
                
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.Seleccionado== null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","Debe seleccionar un elemento",MessageDialogStyle.Affirmative);                    
                }
                else
                {
                    UsuarioView modificarUsuario = new UsuarioView(Instancia);
                    modificarUsuario.ShowDialog();
                }
            }

        }
    }

    
}