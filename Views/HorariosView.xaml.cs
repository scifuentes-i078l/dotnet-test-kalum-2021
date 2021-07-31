using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class HorariosView : MetroWindow
    {

        public HorariosView()
        {
            InitializeComponent();
            HorariosViewModel ModeloDatos = new HorariosViewModel(DialogCoordinator.Instance);
            this.DataContext=ModeloDatos;
        }
    }
}