using System.Windows;
using Kalum2021.ModelView;
using MahApps.Metro.Controls;
namespace Kalum2021.Views
{
    public partial class RolesView : MetroWindow
    {
        public RolesView()
        {
            InitializeComponent();
            RolesViewModel ModeloDatos = new RolesViewModel();
            this.DataContext = ModeloDatos;
        }
    }
}