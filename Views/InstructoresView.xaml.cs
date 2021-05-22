using Kalum2021.ModelView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Kalum2021.Views
{
    public partial class InstructoresView : MetroWindow
    {
        public InstructoresView()
        {
            InitializeComponent();
            InstructoresViewModel ModeloDatos = new InstructoresViewModel(DialogCoordinator.Instance);
            this.DataContext=ModeloDatos;
        }
        
    }
}