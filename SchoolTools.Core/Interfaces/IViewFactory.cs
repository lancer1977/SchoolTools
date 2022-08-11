using System;

namespace PolyhydraGames.SchoolTools.Core.Interfaces
{
    public interface IViewFactory
    {
        void Register(Type type, Type page);
        //void Register<T, T2>();
        object GetPage<T>();
    }
}