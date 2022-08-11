using System.Windows.Input;
using MvvmCross.Platforms.Wpf.Views;
using SchoolTools.Core.Spelling; 

namespace SchoolTools.WPF.Views
{
    /// <summary>
    /// Interaction logic for SpellingView.xaml
    /// </summary>
    public partial class SpellingView : MvxWpfView
    {
        public SpellingView()
        {
            InitializeComponent();
        }

        public new SpellingGameViewModel ViewModel
        {
            get { return (SpellingGameViewModel) base.ViewModel; }
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
       
            // if user didn't press Enter, do nothing
            if (!e.Key.Equals(Key.Enter)) return;

            // execute the command, if it exists
            ViewModel.SubmitGuess.Execute(null);
        }

 

    }
}
