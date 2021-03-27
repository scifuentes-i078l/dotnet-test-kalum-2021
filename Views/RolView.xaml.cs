using System.Windows;
using Kalum2021.ModelView;
using MahApps.Metro.Controls;
namespace Kalum2021.Views
{
    public partial class RolView : MetroWindow
    {
        public RolView(RolesViewModel RolesViewModel)
        {
           InitializeComponent();
           RolViewModel Modelo = new RolViewModel(RolesViewModel);
           this.DataContext= Modelo;

        }
    }
}