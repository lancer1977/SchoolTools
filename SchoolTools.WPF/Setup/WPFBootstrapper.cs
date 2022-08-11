using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Extensions;
using PolyhydraGames.SchoolTools.Core.Interfaces;
using PolyhydraGames.SchoolTools.Core.Setup;

namespace PolyhydraGames.SchoolTools.WPF.Setup
{
    public class WPFBootstrapper : SchoolToolsBootstrapper
    { 
        private WPFBootstrapper()
        {
            Assemblies.Add(typeof(WPFBootstrapper).GetAssembly());
            Modules.Add(new WpfServicesAutofacModule());
         
        }

        public static void Initialize()
        {
            var strap = new WPFBootstrapper();
   

                strap.Setup();
 
        }
        protected override void FollowupAction() { }
        protected override void ValidateRegistration() { }
 
    }
}