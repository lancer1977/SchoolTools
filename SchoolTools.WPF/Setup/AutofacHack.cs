using Autofac;
using ClipboardMonitor.Core;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.ViewModels;
using PolyhydraGames.SchoolTools.WPF.Views;

namespace PolyhydraGames.SchoolTools.WPF.Setup
{
    public class  AutofacHack : AutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {   
            builder.Register(x=>MainWindow.Monitor).As<IClipboardMonitor>().SingleInstance();
            builder.Register(x => MainWindow.Navigator ).As<ISimpleNavigator>().SingleInstance();
        }

    }
}