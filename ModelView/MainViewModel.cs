using System;
using System.ComponentModel;
using System.Windows.Input;
using Kalum2021.Views;

namespace Kalum2021.ModelView
{
    public class MainViewModel : INotifyPropertyChanged, ICommand
    {

        public string Fondo {get;set;} =$"{Environment.CurrentDirectory}\\Images\\fondo2.gif";
        
        public MainViewModel Instancia {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public MainViewModel()
        {
            this.Instancia= this;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parametro)
        {

              
            switch(parametro){
                case "Usuarios":
                    UsuariosView ventanaUsuarios = new UsuariosView();
                    ventanaUsuarios.ShowDialog();
                break;
                case "Roles":
                    RolesView ventanaRoles = new RolesView();
                    ventanaRoles.ShowDialog();
                break;
                case "Alumnos":
                    AlumnosView ventanaAlumnos = new AlumnosView();
                    ventanaAlumnos.ShowDialog();
                break;
                case "Instructores":
                    InstructoresView ventanaInstructores = new InstructoresView();
                    ventanaInstructores.ShowDialog();
                break;
                
                case "Salones":
                    SalonesView ventanaSalones = new SalonesView();
                    ventanaSalones.ShowDialog();
                break;

                 case "Clases":
                    ClasesView ventanaClases = new ClasesView();
                    ventanaClases.ShowDialog();
                    break;
                case "CarrerasTecnicas":
                    CarrerasTecnicasView ventanaCarrerasTecnicas = new CarrerasTecnicasView();
                    ventanaCarrerasTecnicas.ShowDialog();
                break;
                case "Horarios":
                    HorariosView ventanaHorarios = new HorariosView();
                    ventanaHorarios.ShowDialog();
                break;
            }
        }
    }
}