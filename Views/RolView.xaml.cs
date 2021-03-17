using System.Windows;
using Kalum2021.ModelView;

namespace Kalum2021.Views
{
    public partial class RolView : Window
    {
        public RolView(RolesViewModel RolesViewModel)
        {
           InitializeComponent();
           RolViewModel Modelo = new RolViewModel(RolesViewModel);
           this.DataContext= Modelo;

        }
    }
}