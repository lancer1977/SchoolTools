using System;
using System.Collections.Generic;
using System.Windows.Controls;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.SchoolTools.Core.Interfaces;

namespace PolyhydraGames.SchoolTools.WPF.Services
{
    public class ViewFactory : IViewFactory
    {
        public void Register(Type type, Type page)
        {
            _map.Add(type, page);
        }

        public object GetPage<T>()
        {
            var vm = IOC.Get<T>();
            var pageType = _map[typeof(T)];
            var page = IOC.Get(pageType) as Page;
            page.DataContext = vm;
            return page;
        }

        public Dictionary<Type, Type> _map = new Dictionary<Type, Type>();


      
    }
}