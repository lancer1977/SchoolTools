using System.Windows;
using ClipboardMonitor.Core;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Interfaces;
using PolyhydraGames.SchoolTools.Core.Setup;
using PolyhydraGames.SchoolTools.Core.ViewModels;
using PolyhydraGames.SchoolTools.WPF.Setup;

namespace PolyhydraGames.SchoolTools.WPF.Views
{
    public sealed partial class MainWindow : Window, ISimpleNavigator
    {
        public static IClipboardMonitor Monitor { get; private set; }
        public static ISimpleNavigator Navigator { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Monitor = new ClipboardHandler(this);
            Navigator = this;
            WPFBootstrapper.Initialize();


        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigateTo<NoteViewModel>();
        }

        public void NavigateTo<T>()
        {
            var page = IOC.Get<IViewFactory>().GetPage<T>();
            Content = page;
        }
    }
}


