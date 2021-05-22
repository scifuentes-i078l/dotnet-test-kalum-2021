using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class ClasesView : MetroWindow
    {
        public ClasesView()
        {
            InitializeComponent();
            ClasesViewModel ModeloDatos = new ClasesViewModel(DialogCoordinator.Instance);
            this.DataContext=ModeloDatos;
        }
        
    }
}