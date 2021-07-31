using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.DataContext;
using Kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace Kalum2021.ModelView
{
    public class ClaseViewModel : INotifyPropertyChanged, ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;


        public ClaseViewModel Instancia {get;set;}

        public ClasesViewModel  ClasesViewModel {get;set;}

         public String Titulo {get;set;}

        public IDialogCoordinator dialogCoordinator;

        private KalumDBContext dBcontext;


        

            private List<int> _CupoMinimo;

  public List <int> CupoMinimo {
            get{
                if (this._CupoMinimo==null)
                {
                    this._CupoMinimo= new List<int>();
                    int i = 1;
                     do {                   
                        this._CupoMinimo.Add(i);
                        i++;
                     } while (i < 30);                
                }               
                return this._CupoMinimo;
            }
            set{
                this._CupoMinimo=value;
            }
            }

private List<int> _CupoMaximo;

        public List <int> CupoMaximo {
            get{
                if (this._CupoMaximo==null){
                    this._CupoMaximo= new List<int>();
                    int i = 0;
                while (i < 30){
                    i++;
                    this._CupoMaximo.Add(i);
                }
                }

                return this._CupoMaximo;

            }
            set{
                this._CupoMaximo=value;
            }
            }
        private List <int> _Ciclos;

        public List <int> Ciclos {
            get{
                if (_Ciclos==null){
                    this._Ciclos= new List<int> ();
                    for (int i = 2020;i <= 2030;i++){
                        this._Ciclos.Add(i);
                    }
                  
                }
                return this._Ciclos;

            }
            set{
                this._Ciclos=value;
            }
            }
        public bool CanExecute(object parametro)
        {
            return true;
        }

        public ObservableCollection<CarreraTecnica> CarrerasTecnicas {get;set;}
        public ObservableCollection<Instructor> Instructores {get;set;}

        
        public ObservableCollection<Salon> Salones {get;set;}

        
        public ObservableCollection<Horario> Horarios {get;set;}

        public CarreraTecnica CarreraTecnicaSeleccionada {get;set;}

        public Instructor InstructorSeleccionado {get;set;}

        public Salon SalonSeleccionado {get;set;} 

        public Horario HoarioSeleccionado {get;set;}
        
        public String ValorDescripcion {get;set;}

        public String ValorCiclo {get;set;} ="0";


        public String ValorCupoMaximoSeleccionado {get;set;} ="0";

        public String ValorCupoMinimoSeleccionado {get;set;} ="0";

        public string InstructorDefinido {get;set;} = "SELECCIONAR";
        public string HorarioDefinido {get;set;} = "SELECCIONAR";
        public string SalonDefinido {get;set;} = "SELECCIONAR";
        public string CarreraTecnicaDefinido {get;set;} = "SELECCIONAR";

        public ClaseViewModel(ClasesViewModel ClasesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator=instance;
            this.Instancia=this;
            this.ClasesViewModel=ClasesViewModel;
            this.dBcontext= new KalumDBContext();
            this.CarrerasTecnicas= new ObservableCollection<CarreraTecnica>(this.dBcontext.CarrerasTecnicas.ToList());
            this.Instructores= new ObservableCollection<Instructor>(this.dBcontext.Instructores.ToList());

            this.Horarios= new ObservableCollection<Horario>(this.dBcontext.Horarios.ToList());
        this.Salones= new ObservableCollection<Salon>(this.dBcontext.Salones.ToList());

        this.Titulo="Nuevo Registro";
            if (this.ClasesViewModel.Seleccionado!=null){
            this.Titulo="Modificar Registro";
            this.ValorDescripcion=this.ClasesViewModel.Seleccionado.Descripcion;
            this.ValorCiclo=this.ClasesViewModel.Seleccionado.Ciclo.ToString();
            this.ValorCupoMaximoSeleccionado=this.ClasesViewModel.Seleccionado.CupoMaximo.ToString();
            this.ValorCupoMinimoSeleccionado=this.ClasesViewModel.Seleccionado.CupoMinimo.ToString();
            this.CarreraTecnicaSeleccionada=this.ClasesViewModel.Seleccionado.CarreraTecnica;
            this.SalonSeleccionado=this.ClasesViewModel.Seleccionado.Salon;
            this.HoarioSeleccionado=this.ClasesViewModel.Seleccionado.Horario;
            this.InstructorSeleccionado=this.ClasesViewModel.Seleccionado.Instructor;
            this.InstructorDefinido = this.ClasesViewModel.Seleccionado.Instructor.Apellidos + " " + this.ClasesViewModel.Seleccionado.Instructor.Nombres;
                this.CarreraTecnicaDefinido = this.ClasesViewModel.Seleccionado.CarreraTecnica.Nombre;
                this.SalonDefinido = this.ClasesViewModel.Seleccionado.Salon.NombreSalon;
                this.HorarioDefinido = $"{this.ClasesViewModel.Seleccionado.Horario.HorarioInicio.ToString(@"hh\:mm")} - {this.ClasesViewModel.Seleccionado.Horario.HorarioFinal:hh\\:mm}";
            
            }
        }
        
        public async void Execute(object parametro)
        {

            if (parametro is Window)
            {
                try {
                    if (this.ClasesViewModel.Seleccionado  == null){        

                        Clase NuevaClase = new Clase();
                        NuevaClase.ClaseId = Guid.NewGuid().ToString();
                        NuevaClase.Descripcion= ValorDescripcion;
                        NuevaClase.Ciclo= Convert.ToInt16(ValorCiclo);
                        NuevaClase.CupoMaximo = Convert.ToInt16(ValorCupoMaximoSeleccionado);
                        NuevaClase.CupoMinimo = Convert.ToInt16(ValorCupoMinimoSeleccionado);
                        NuevaClase.Instructor=InstructorSeleccionado;
                        NuevaClase.Salon=SalonSeleccionado;
                        NuevaClase.Horario=HoarioSeleccionado;
                        NuevaClase.CarreraTecnica=CarreraTecnicaSeleccionada;

                        dBcontext.Clases.Add(NuevaClase);
                        dBcontext.SaveChanges();
                         ClasesViewModel.Listado.Add(NuevaClase);
                         await this.dialogCoordinator.ShowMessageAsync(this,"Clases","registro agregado");
                        
                        
                    }else{

                        int posicion = this.ClasesViewModel.Listado.IndexOf(this.ClasesViewModel.Seleccionado);
                        Clase temporal = new Clase();
                        temporal.ClaseId= this.ClasesViewModel.Seleccionado.ClaseId;
                        temporal.Descripcion=this.ValorDescripcion;
                        temporal.Ciclo=Convert.ToInt16(this.ValorCiclo);
                        temporal.CupoMaximo=Convert.ToInt16(this.ValorCupoMaximoSeleccionado);
                        temporal.CupoMinimo=Convert.ToInt16(this.ValorCupoMinimoSeleccionado);
                        temporal.CarreraId=this.CarreraTecnicaSeleccionada.CarreraId;
                        temporal.InstructorId=this.InstructorSeleccionado.InstructorId;
                        temporal.SalonId=this.SalonSeleccionado.SalonId;
                        temporal.HorarioId=this.HoarioSeleccionado.HorarioId;
                        this.dBcontext.Entry(temporal).State=EntityState.Modified;
                        this.ClasesViewModel.Listado.RemoveAt(posicion);
                        this.ClasesViewModel.Listado.Insert(posicion,temporal);
                        this.dBcontext.SaveChanges();
                        await this.dialogCoordinator.ShowMessageAsync(this,"Clases","registro actualizado");
                  
                       


                    }
                    
                    //await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","registro actualizado");
                 }catch (Exception e){
                     await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                 }
                ((Window)parametro).Close();
            }

        }

        
            
    }
}