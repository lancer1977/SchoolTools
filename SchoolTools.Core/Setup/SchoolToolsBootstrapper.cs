using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection; 
using PolyhydraGames.Core.IOC; 
using PolyhydraGames.Extensions;
using PolyhydraGames.SchoolTools.Core.Interfaces;

namespace PolyhydraGames.SchoolTools.Core.Setup
{
    public abstract class SchoolToolsBootstrapper : IoCBootstrapper
    {

        protected void PairMVVMInterfaces(IList<Type> pages, IList<Type> viewModels)
        {
            var factory = IOC.Get<IViewFactory>();
            foreach (var page in pages)
            {
                var prefix = page.GetTypeInfo().Name.Replace("Page", "");
                var viewModel =
                    viewModels.FirstOrDefault(i => i.GetTypeInfo().Name.Replace("ViewModel", "") == prefix);
                if (viewModel == null)
                {
                    Debug.WriteLine($"No viewmodel found for type of {page.Name}");
                    //throw new Exception($"No viewmodel found for type of {page.Name}");
                    continue;

                }

                Debug.WriteLine($"Registring {viewModel.Name} to {page.Name}");
                factory.Register(viewModel, page);
            }
        }

        protected override void ViewFactoryRegistration()
        {
            var pages = Pages(Assemblies.ToArray()).ToArray();
            var viewModels = ViewModels(Assemblies.ToArray());
            //PairMVVM(pages, viewModels.ToArray());
            PairMVVMInterfaces(pages, viewModels.ToArray());
        }

        protected SchoolToolsBootstrapper()
        {
            IOC.Initialize(new AutofacIOC());
            Assemblies.Add(typeof(SchoolToolsBootstrapper).GetAssembly());

        }
    }
}