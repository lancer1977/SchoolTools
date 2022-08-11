using System.Collections.Generic;
using System.Windows;
using ClipboardMonitor.Core;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using ITextToSpeech = PolyhydraGames.TextToSpeech.Core.ITextToSpeech;
using PolyhydraGames.SchoolTools.Core.ViewModels;
using PolyhydraGames.SchoolTools.WPF.Services;
using PolyhydraGames.SchoolTools.WPF.Views;
using PolyhydraGames.SQLite;
using PolyhydraGames.TextToSpeech.Core;

namespace PolyhydraGames.SchoolTools.WPF.Setup
{
    public class WpfServicesAutofacModule : IIOCRegistrationModule
    {
        public void Load(IList<IIOCRegistration> builder)
        {  
          
            builder.Add(new AutofacHack());
            builder.RegisterType<TextToSpeech.Windows.TextToSpeech>().As<ITextToSpeech>().SingleInstance(); 
            builder.RegisterType<MainThreadDispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ViewFactory>().AsImplementedInterfaces().SingleInstance(); 
            builder.RegisterType<SqlLiteBridge>().AsImplementedInterfaces().SingleInstance(); 
            builder.RegisterType<Storage>().AsImplementedInterfaces().SingleInstance(); 
            
        } 
    }
}


