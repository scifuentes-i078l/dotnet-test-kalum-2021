using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class SalonesView : MetroWindow
    {
        public SalonesView()
        {
            InitializeComponent();
            SalonesViewModel ModeloDatos = new SalonesViewModel(DialogCoordinator.Instance);
            this.DataContext=ModeloDatos;
        }
        
    }
}