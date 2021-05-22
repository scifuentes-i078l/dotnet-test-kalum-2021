using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class AlumnoView: MetroWindow    
    {
         public AlumnoView(AlumnosViewModel AlumnosViewModelInstance)
         {
            InitializeComponent();
            AlumnoViewModel Modelo = new AlumnoViewModel(AlumnosViewModelInstance, DialogCoordinator.Instance);
            this.DataContext = Modelo;
         }
         
        
    }
}