using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.Models;
using Kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class InstructoresViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ObservableCollection<Instructor> Listado {get;set;}
        public Instructor Seleccionado {get;set;}

        public InstructoresViewModel Instancia {get;set;}
        
        public IDialogCoordinator dialogCoordinator;

        private const string tituloVentana="Instructor";

        public InstructoresViewModel(IDialogCoordinator instanceIDialog)
        {
            this.Instancia=this;
            this.dialogCoordinator=instanceIDialog;
            this.Listado= new ObservableCollection<Instructor>();
            this.Listado.Add(new Instructor("1","nombres","apellidos","comentario","direccionn","estatus","foto","telefono"));            
        }

        public void agregarElemento(Instructor nuevoElemento)
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
                InstructorView vistaNuevo= new InstructorView(Instancia);
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
                    InstructorView vistaModificar = new InstructorView(Instancia);
                    vistaModificar.ShowDialog();
                }
            }
        }
    }
}