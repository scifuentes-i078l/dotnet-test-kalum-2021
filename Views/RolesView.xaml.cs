using System.Windows;
using Kalum2021.ModelView;

namespace Kalum2021.Views
{
    public partial class RolesView : Window
    {
        public RolesView()
        {
            InitializeComponent();
            RolesViewModel ModeloDatos = new RolesViewModel();
            this.DataContext = ModeloDatos;
        }
    }
}