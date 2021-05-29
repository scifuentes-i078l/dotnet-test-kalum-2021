using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class CarreraTecnicaView: MetroWindow    
    {
         public CarreraTecnicaView(CarrerasTecnicasViewModel CarrerasTecnicasViewModelInstance)
         {
            InitializeComponent();
            CarreraTecnicaViewModel Modelo = new CarreraTecnicaViewModel(CarrerasTecnicasViewModelInstance, DialogCoordinator.Instance);
            this.DataContext = Modelo;
         }
         
        
    }
}