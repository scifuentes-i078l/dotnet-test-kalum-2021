using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.ModelView
{
    public class InstructorViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public InstructorViewModel Instancia {get;set;}

        public InstructoresViewModel InstructoresViewModel {get;set;}

       
    public string Instructor_id {get; set;}
    public string  Nombres {get; set;}
    public string Apellidos {get; set;}
    public string  Comentario {get; set;}
    public string  Direccion {get; set;}
    public string  Estatus {get; set;}
    public string  Foto {get; set;}
    public string Telefono {get; set;}

        public Instructor instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        public InstructorViewModel(InstructoresViewModel InstructoresViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.InstructoresViewModel=InstructoresViewModel;
            if (this.InstructoresViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new Instructor();
                this.Instructor_id=this.InstructoresViewModel.Seleccionado.Instructor_id;
                this.Nombres=this.InstructoresViewModel.Seleccionado.Nombres;
                this.Apellidos=this.InstructoresViewModel.Seleccionado.Apellidos;
                this.Comentario=this.InstructoresViewModel.Seleccionado.Comentario;             
				this.Direccion=this.InstructoresViewModel.Seleccionado.Direccion;             
				this.Estatus=this.InstructoresViewModel.Seleccionado.Estatus;             
				this.Foto=this.InstructoresViewModel.Seleccionado.Foto;             
				this.Telefono=this.InstructoresViewModel.Seleccionado.Telefono;             
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

                if (this.InstructoresViewModel.Seleccionado  == null){                    
                    Instructor nuevo = new Instructor(Instructor_id,Nombres,Apellidos,Comentario,Direccion,Estatus,Foto,Telefono);                                        
                    this.InstructoresViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Instructor","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.Instructor_id=this.Instructor_id;
					this.instanciaTemporal.Nombres=this.Nombres;
					this.instanciaTemporal.Apellidos=this.Apellidos;
					this.instanciaTemporal.Comentario=this.Comentario;
					this.instanciaTemporal.Direccion=this.Direccion;
					this.instanciaTemporal.Estatus=this.Estatus;
					this.instanciaTemporal.Foto=this.Foto;
					this.instanciaTemporal.Telefono=this.Telefono;
                    int posicision = this.InstructoresViewModel.Listado.IndexOf(this.InstructoresViewModel.Seleccionado);
                    this.InstructoresViewModel.Listado.RemoveAt(posicision);
                    this.InstructoresViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Instructor","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}