using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class HorarioView: MetroWindow
    {
        public HorarioView(HorariosViewModel HorariosViewModelInstance)
        {
            InitializeComponent();
            HorarioViewModel Modelo = new HorarioViewModel(HorariosViewModelInstance, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
        
    }
}