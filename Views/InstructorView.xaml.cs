using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class InstructorView: MetroWindow    
    {
         public InstructorView(InstructoresViewModel InstructoresViewModelInstance)
         {
            InitializeComponent();
            InstructorViewModel Modelo = new InstructorViewModel(InstructoresViewModelInstance, DialogCoordinator.Instance);
            this.DataContext = Modelo;
         }
         
        
    }
}