using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class SalonView: MetroWindow    
    {
         public SalonView(SalonesViewModel SalonesViewModelInstance)
         {
            InitializeComponent();
            SalonViewModel Modelo = new SalonViewModel(SalonesViewModelInstance, DialogCoordinator.Instance);
            this.DataContext = Modelo;
         }
         
        
    }
}