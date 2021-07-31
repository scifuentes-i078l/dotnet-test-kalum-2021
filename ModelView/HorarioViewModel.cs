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
    public class HorarioViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;


         public HorarioViewModel Instancia {get;set;}

        public HorariosViewModel HorariosViewModel {get;set;}

        public TimeSpan  HorarioInicio{get;set;}
        public TimeSpan  HorarioFinal{get;set;}        
        public Horario instanciaTemporal {get;set;}

        public IDialogCoordinator dialogCoordinator;

        private KalumDBContext dBcontext;

        public String Titulo {get;set;}

        public HorarioViewModel(HorariosViewModel HorariosViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.HorariosViewModel=HorariosViewModel;
            this.Titulo="Nuevo Registro";
            if (this.HorariosViewModel.Seleccionado!=null)
            {
                instanciaTemporal = new Horario();
                this.HorarioInicio=this.HorariosViewModel.Seleccionado.HorarioInicio;
                this.HorarioFinal=this.HorariosViewModel.Seleccionado.HorarioFinal;
                this.Titulo="Modificar Registro";           
            }
            this.dBcontext= new KalumDBContext();
        }



        public bool CanExecute(object parameter)
        {
             return true;
        }

        public async void Execute(object parametro)
        {
           if (parametro is Window)
            {
            try {
                if (this.HorariosViewModel.Seleccionado  == null){                    
                    Horario nuevo = new Horario();                                        
                    nuevo.HorarioId=Guid.NewGuid().ToString();
                    nuevo.HorarioInicio=HorarioInicio;
                    nuevo.HorarioFinal=HorarioFinal;
                    this.HorariosViewModel.agregarElemento(nuevo);
                     this.dBcontext.Horarios.Add(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Horario","Elemento agregado correctamente!!!",MessageDialogStyle.Affirmative);
                }else{
                    this.instanciaTemporal.HorarioId=this.HorariosViewModel.Seleccionado.HorarioId;
                    this.instanciaTemporal.HorarioInicio=this.HorarioInicio;
                    this.instanciaTemporal.HorarioFinal=this.HorarioFinal;
                    int posicision = this.HorariosViewModel.Listado.IndexOf(this.HorariosViewModel.Seleccionado);
                    this.HorariosViewModel.Listado.RemoveAt(posicision);
                    this.HorariosViewModel.Listado.Insert(posicision,instanciaTemporal);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificar Horario","Elemento modificado correctamente!!!",MessageDialogStyle.Affirmative);
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