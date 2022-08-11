using System.Windows.Controls;
using PolyhydraGames.SchoolTools.Core.Math;

namespace PolyhydraGames.SchoolTools.WPF.Views
{
    /// <summary>
    /// Interaction logic for NoteManagerView.xaml
    /// </summary>
    public partial class MathPage : Page
    {

        public MathViewModel ViewModel => (MathViewModel)base.DataContext;


        public MathPage()
        {
            InitializeComponent();

        }


    }
}
