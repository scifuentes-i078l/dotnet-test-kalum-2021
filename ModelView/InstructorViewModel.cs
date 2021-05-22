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
    public class InstructorViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public InstructorViewModel Instancia {get;set;}

        public InstructoresViewModel InstructoresViewModel {get;set;}

       
    public string InstructorId {get; set;}
    public string  Nombres {get; set;}
    public string Apellidos {get; set;}
    public string  Comentario {get; set;}
    public string  Direccion {get; set;}
    public string  Estatus {get; set;}
    public string  Foto {get; set;}
    public string Telefono {get; set;}

        public Instructor instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        private KalumDBContext dBcontext;

    public String Titulo {get;set;}    

        public InstructorViewModel(InstructoresViewModel InstructoresViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.InstructoresViewModel=InstructoresViewModel;
            this.Titulo="Nuevo Registro";
            if (this.InstructoresViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new Instructor();
                this.InstructorId=this.InstructoresViewModel.Seleccionado.InstructorId;
                this.Nombres=this.InstructoresViewModel.Seleccionado.Nombres;
                this.Apellidos=this.InstructoresViewModel.Seleccionado.Apellidos;
                this.Comentario=this.InstructoresViewModel.Seleccionado.Comentario;             
				this.Direccion=this.InstructoresViewModel.Seleccionado.Direccion;             
				this.Estatus=this.InstructoresViewModel.Seleccionado.Estatus;             
				this.Foto=this.InstructoresViewModel.Seleccionado.Foto;             
				this.Telefono=this.InstructoresViewModel.Seleccionado.Telefono;  
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

                if (this.InstructoresViewModel.Seleccionado  == null){                    
                    Instructor nuevo = new Instructor(InstructorId,Nombres,Apellidos,Comentario,Direccion,Estatus,Foto,Telefono);                                        
                    this.InstructoresViewModel.agregarElemento(nuevo);
                    this.dBcontext.Instructores.Add(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Instructor","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.InstructorId=this.InstructoresViewModel.Seleccionado.InstructorId;
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