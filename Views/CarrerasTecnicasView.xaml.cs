using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class CarrerasTecnicasView : MetroWindow
    {
        public CarrerasTecnicasView()
        {
            InitializeComponent();
            CarrerasTecnicasViewModel ModeloDatos = new CarrerasTecnicasViewModel(DialogCoordinator.Instance);
            this.DataContext=ModeloDatos;
        }
        
    }
}