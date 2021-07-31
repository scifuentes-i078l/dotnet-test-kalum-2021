using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class ClaseView : MetroWindow
    {
        public ClaseView(ClasesViewModel clasesViewModel)
        {
            InitializeComponent();

            ClaseViewModel Modelo = new ClaseViewModel(clasesViewModel,DialogCoordinator.Instance);
            this.DataContext=Modelo;            
        }
    }
}